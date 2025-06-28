using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class ChiTietDatTiec
    {
        //Primary Key and column attributes
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Mã dịch vụ không được để trống")]
        public string TrangThai { get; set; }


        // Foreign keys
        public Guid MaMon { get; set; }
        public MonAn? MonAn { get; set; }
        public Guid MaHD { get; set; }
        public HopDong? HopDong { get; set; }

    }
}
