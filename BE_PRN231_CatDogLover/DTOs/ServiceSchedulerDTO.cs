using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ServiceSchedulerDTO
    {
        [Required]
        [MaxLength(50)]
        public string ItemId { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string ServiceId { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
