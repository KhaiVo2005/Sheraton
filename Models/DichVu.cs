using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class DichVu
    {
        //Primary key and properties
        [Key]
        public Guid MaDV { get; set; } 
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        public string TenDV { get; set; } 
        [Required(ErrorMessage = "Đơn giá không được để trống")]
        public decimal DonGia { get; set; } 
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string MoTa { get; set; }

        // Navigation properties
        public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
    }
}
