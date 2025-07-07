using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class HopDong
    {
        // Primary key and column attributes
        [Key]
        public Guid MaHD { get; set; }
        [Required(ErrorMessage = "Ngày ký hợp đồng không được để trống")]
        public DateTime NgayKy { get; set; }
        [Required(ErrorMessage = "Tiền cọc không được để trống")]
        public decimal TienCoc { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }
        public string? TrangThai { get; set; } = "Chưa thanh toán";
        public string? PTTT { get; set; }
        

        // Foreign keys
        public Guid MaKH { get; set; }
        public KhachHangg? KhachHang { get; set; }
        public Guid MaNV { get; set; }
        public NhanVien? NhanVien { get; set; }

        // Navigation properties
        public ICollection<LichDatSanh> LichDatSanhs { get; set; } = new List<LichDatSanh>();
        public ICollection<ChiTietDatTiec> ChiTietDatTiecs { get; set; } = new List<ChiTietDatTiec>();
        public ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    }
}
