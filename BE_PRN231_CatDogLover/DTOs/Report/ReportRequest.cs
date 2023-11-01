using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Report
{
    public class ReportRequest
    {
        public int ReporterId { get; set; }
        public int ReportedPersonId { get; set; }
        public string? Content { get; set; }
    }
}
