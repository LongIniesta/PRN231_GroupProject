using BusinessObjects;
using DTOs.Report;
using DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IReportRepository
    {
        Task AddReport(Report Report);
        Task RemoveReport(int reporterId, int reportedId);
        Task UpdateReport(Report Report);
        Task<Report?> GetReportById(int reporterId, int reportedId);
        IEnumerable<Report> GetAll();
        PagedList<Report> Search(ReportSearchRequest searchRequest);
    }
}
