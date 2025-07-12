using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sheraton.Data;
using Sheraton.Models;
using Sheraton.Models.ViewModel;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;

namespace Sheraton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        private readonly IVnpay _vnpay;
        SheratonDbContext _context;
        IOptions<VnpayOptions> _options;
        Guid _maHD;

        public VnpayController(IOptions<VnpayOptions> options, SheratonDbContext context)
        {
            _options = options;
            _vnpay = new Vnpay();
            _context = context;
        }

        [HttpGet("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl(double moneyToPay, string description, Guid maHD)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                string callbackUrl = $"https://localhost:7136/api/Vnpay/Callback?maHD={maHD}";

                _vnpay.Initialize(_options.Value.TmnCode, _options.Value.HashSecret, _options.Value.BaseUrl, callbackUrl);

                var request = new PaymentRequest
                {
                    PaymentId = DateTime.Now.Ticks,
                    Money = moneyToPay,
                    Description = description,
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY,
                    CreatedDate = DateTime.Now,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };

                var paymentUrl = _vnpay.GetPaymentUrl(request);

                return Ok(paymentUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                    if (paymentResult.IsSuccess)
                    {
                        return Ok();
                    }

                    return BadRequest("Thanh toán thất bại");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }

        [HttpGet("Callback")]
        public IActionResult Callback(Guid maHD)
        {
            try
            {
                _vnpay.Initialize(_options.Value.TmnCode, _options.Value.HashSecret, _options.Value.BaseUrl, _options.Value.CallbackUrl);

                var vnpQuery = new QueryCollection(
                Request.Query
                    .Where(q => q.Key.StartsWith("vnp_"))
                    .ToDictionary(q => q.Key, q => q.Value)
                );

                Console.WriteLine($"Callback received for maHD: {maHD}");

                var paymentResult = _vnpay.GetPaymentResult(vnpQuery);

                var hopDong = _context.HopDongs.FirstOrDefault(h => h.MaHD == maHD);

                // Nếu thanh toán thành công, redirect về trang ChiTietHoaDon với maHD
                if (paymentResult.IsSuccess)
                {
                    hopDong.TrangThai = "Đã thanh toán";
                    _context.SaveChanges();

                    return Redirect($"https://localhost:7136/Accounting/Home/detailsHoaDon?id={maHD}");
                }


                // Nếu thất bại
                return Redirect($"https://localhost:7136/Accounting/Home/detailsHoaDon?id={maHD}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
