using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class LichDatSanh
    {
        // Primary key and column attributes
        [Key]
        public Guid MaLDS { get; set; }
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public DateTime BatDau { get; set; }
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public DateTime KetThuc { get; set; }
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public string TrangThai { get; set; }

        // Foreign keys
        public Guid MaSanh { get; set; }
        public SanhTiec? Sanh { get; set; }
        public Guid MaHD { get; set; }
        public HopDong? HopDong { get; set; }

        public ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();
    }
}
