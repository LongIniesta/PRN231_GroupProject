using DTOs.ValidateCustom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class PostDTO
    {
        public int? PostId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        [TypePost]
        public string Type { get; set; } = null!;
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool Status { get; set; }
        public int? NumberOfReact { get; set; }
        public bool? Reacted { get; set; }

        public virtual AccountDTO? Owner { get; set; } = null!;
        public virtual ICollection<GiftDTO>? Gifts { get; set; }
        public virtual ICollection<ProductDTO>? Products { get; set; }
        public virtual ICollection<ServiceDTO>? Services { get; set; }
        public virtual ICollection<ReactDTO>? Reacts { get; set; }
    }
}
