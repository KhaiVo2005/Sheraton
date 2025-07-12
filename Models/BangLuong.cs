using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class BangLuong
    {
        [Key]
        public Guid MaBL { get; set; }
        public decimal TongGioLam { get; set; }
        public decimal? TongLuong { get; set; }
        public Guid MaNV { get; set; }
        
        public NhanVien? NhanVien { get; set; }
    }
}
