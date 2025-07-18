using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class SanhTiec
    {
        // Primary key and column attributes
        [Key]
        public Guid MaSanh { get; set; }
        [Required(ErrorMessage = "Tên sảnh tiệc không được để trống")]
        public string TenSanh { get; set; }
        [Required(ErrorMessage = "Sức chứa không được để trống")]
        public int SucChua { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "Giá không được để trống")]
        public decimal Gia { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }
        public string? HinhAnh { get; set; }

        // Navigation properties
        public ICollection<LichDatSanh> LichDatSanhs { get; set; } = new List<LichDatSanh>();
    }
}
