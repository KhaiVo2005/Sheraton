using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class DatHang
    {
        // Primary key and column attributes
        [Key]
        public Guid MaDatHang { get; set; } 
        [Required(ErrorMessage = "Tên mặt hàng không được để trống")]
        public string TenMH { get; set; } 
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string GhiChu { get; set; }

        // Navigation properties
        //public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}
