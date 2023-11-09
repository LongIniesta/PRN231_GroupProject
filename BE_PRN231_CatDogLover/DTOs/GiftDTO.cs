using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class GiftDTO
    {
        [Required]
        [MaxLength(50)]
        public string GiftId { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string GiftName { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public int PostId { get; set; }
        [MaxLength(500)]
        public string? ImageLink { get; set; }
        [Required]
        public bool Status { get; set; }

        public virtual ICollection<GiftCommentDTO>? GiftComments { get; set; }
    }
}
