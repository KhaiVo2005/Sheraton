using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class ChiTietDatTiec
    {
        //Primary Key and column attributes
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        [Column(TypeName = "nvarchar(50)")]
        public string TrangThai { get; set; } = "Chưa phục vụ";


        // Foreign keys
        public Guid? MaMon { get; set; }
        public MonAn? MonAn { get; set; }
        public Guid MaHD { get; set; }
        public HopDong? HopDong { get; set; }
        
    }
}
