using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class KhachHang
    {
        // Primary key and column attributes
        [Key]
        public Guid MaKH { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public bool GioiTinh { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Email { get; set; }

        // Navigation properties
        public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}
