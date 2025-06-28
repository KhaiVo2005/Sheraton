using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheraton.Models
{
    public class PhanCong
    {
        // column attributes
        [Required(ErrorMessage = "Ca làm việc không được để trống")]
        public DateTime CaLamViec { get; set; }
        [Required(ErrorMessage = "Ghi chú không được để trống")]
        public string GhiChu { get; set; }

        // Foreign keys
        public Guid MaNV { get; set; }
        public NhanVien? NhanVien { get; set; }
        public Guid MaLDS { get; set; }
        public LichDatSanh? LichDatSanh { get; set; }

    }
}
