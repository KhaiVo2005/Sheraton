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
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        // Foreign keys
        public Guid MaKH { get; set; }
        public KhachHang? KhachHang { get; set; }
        public Guid MaNV { get; set; }
        public NhanVien? NhanVien { get; set; }

        // Navigation properties
        public ICollection<LichDatSanh> LichDatSanhs { get; set; } = new List<LichDatSanh>();
        public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public ICollection<ChiTietDatTiec> ChiTietDatTiecs { get; set; } = new List<ChiTietDatTiec>();
        public ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    }
}
