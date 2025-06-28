using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class ChiTietDichVu
    {
        // Column atributes
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }

        // Foreign keys
        public Guid MaHD { get; set; }
        public HopDong? HopDong { get; set; }
        public Guid MaDV { get; set; }
        public DichVu? DichVu { get; set; }
    }
}
