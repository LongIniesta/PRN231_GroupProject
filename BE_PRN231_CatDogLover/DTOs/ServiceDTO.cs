using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ServiceDTO
    {
        [Required]
        [MaxLength(50)]
        public string ServiceId { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string? ServiceName { get; set; }
        [Required]
        public int PostId { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public bool Status { get; set; }
        [MaxLength(500)]
        public string? ImageLink { get; set; }
        [MaxLength(500)]
        [Required]
        public string? Address { get; set; }

        public virtual ICollection<ServiceSchedulerDTO> ServiceSchedulers { get; set; }
    }
}
