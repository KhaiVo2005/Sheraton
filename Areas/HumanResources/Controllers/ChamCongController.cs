using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebTravel.Attribute;

namespace Sheraton.Areas.HumanResources.Controllers
{
    [CheckRole("HumanResources")]
    [Area("HumanResources")]
    public class ChamCongController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SheratonDbContext _context;

        public ChamCongController(IHttpClientFactory httpClientFactory, SheratonDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        [HttpGet]
        public IActionResult UploadChamCong()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadChamCong(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Error = "Vui lòng chọn file Excel.";
                return View(new List<BangLuong>());
            }

            try
            {
                using var client = _httpClientFactory.CreateClient();
                using var content = new MultipartFormDataContent();
                using var fileStream = file.OpenReadStream();
                using var streamContent = new StreamContent(fileStream);

                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                content.Add(streamContent, "file", file.FileName);

                var response = await client.PostAsync("http://localhost:5000/tinh-tong-gio", content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Gọi API thất bại.";
                    return View();
                }

                var resultJson = await response.Content.ReadAsStringAsync();

                // Deserialize tạm để xử lý
                var dsLuongTam = JsonSerializer.Deserialize<List<BangLuongTam>>(resultJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dsLuongTam == null || !dsLuongTam.Any())
                {
                    ViewBag.Error = "API trả về dữ liệu rỗng.";
                    return View();
                }

                const decimal DON_GIA = 5000m;

                var dsLuong = dsLuongTam
                    .Where(x => x.MaNV != Guid.Empty)
                    .Select(x => new BangLuong
                    {
                        MaBL = Guid.NewGuid(),
                        MaNV = x.MaNV,
                        TongGioLam = x.TongGioLam,
                        TongLuong = x.TongGioLam * DON_GIA
                    })
                    .ToList();

                _context.BangLuongs.AddRange(dsLuong);
                await _context.SaveChangesAsync();

                ViewBag.KetQua = resultJson;
                var bangLuongs = await _context.BangLuongs
                    .Include(b => b.NhanVien) // nếu muốn hiển thị cả tên nhân viên
                    .ToListAsync();
                return View(bangLuongs);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi xử lý: " + ex.InnerException?.Message ?? ex.Message;
                return View();
            }

        }

        // Class trung gian để đọc JSON từ API
        private class BangLuongTam
        {
            public Guid MaNV { get; set; }
            public decimal TongGioLam { get; set; }
        }
    }
}
