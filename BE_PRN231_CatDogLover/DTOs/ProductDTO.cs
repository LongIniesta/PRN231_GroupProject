using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductDTO
    {
        [Required]
        [MaxLength(50)]
        public string ItemId { get; set; } = null!;
        [Key]
        [MaxLength(50)]
        public string ProductId { get; set; } = null!;
        [Required]
        public int PostId { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string? ImageLink { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public bool Status { get; set; }

        public virtual CategoryDTO? Category { get; set; } = null!;
    }
}
