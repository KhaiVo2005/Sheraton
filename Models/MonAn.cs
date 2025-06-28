using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class MonAn
    {
        // Primary Key and column attributes
        [Key]
        public Guid MaMon { get; set; } 
        [Required(ErrorMessage = "Tên món ăn không được để trống")]
        public string TenMon { get; set; }
        [Required(ErrorMessage = "Loại món ăn không được để trống")]
        public decimal DonGia { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string MoTa { get; set; }

        // Navigation properties
        public ICollection<ChiTietDatTiec> ChiTietDatTiecs { get; set; } = new List<ChiTietDatTiec>();
    }
}
