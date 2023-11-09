using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ReactDTO
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int ReactTypeId { get; set; }
        public virtual ReactTypeDTO ReactType { get; set; } = null!;
    }
}
