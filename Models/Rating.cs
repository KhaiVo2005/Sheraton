using System.ComponentModel.DataAnnotations;

namespace Sheraton.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Range(1, 5)]
        [Required]
        public int Score { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // FK: HopDong
        public Guid? MaHD { get; set; }
        public HopDong? HopDong { get; set; }
    }
}
