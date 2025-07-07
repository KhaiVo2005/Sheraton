using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class NhanVien
    {
        // Primary key and column attributes
        [Key]
        public Guid MaNV { get; set; }
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        public string TenNV { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public bool GioiTinh { get; set; }
        [Required(ErrorMessage = "Chức vụ không được để trống")]
        public string ChucVu { get; set; }

        // Navigation properties
        public ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();
        public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
        public ICollection<DatHang> DatHangs { get; set; } = new List<DatHang>();
    }
}
