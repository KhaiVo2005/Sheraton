using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class HoaDon
    {
        // Primary key and column attributes
        [Key]
        public Guid MaHD { get; set; } 
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public string LoaiHD { get; set; } 
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public string PTTT { get; set; } 
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        public string TrangThai { get; set; } 
        [Required(ErrorMessage = "Mã hợp đồng không được để trống")]

        // Foreign keys
        public Guid MaHopDong { get; set; }
        public HopDong? HopDong { get; set; }
        public Guid MaDH { get; set; }
        public DatHang? DatHang { get; set; }
        public Guid MaNV { get; set; }
        public NhanVien? NhanVien { get; set; }
    }
}
