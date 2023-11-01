using BusinessObjects;
using DataAccess;
using DTOs.Pagination;
using DTOs.Report;
using Repositories.Interface;
using Repositories.Utility;

namespace Repositories
{
    public class ReportRepository : IReportRepository
    {
        public async Task AddReport(Report report)
        {
            try
            {
                var checkDuplicate = await ReportDAO.Instance.GetByID(report.ReporterId, report.ReportedPersonId);
                if (checkDuplicate != null) throw new Exception("Duplicated!");

                var checkExistedReportedPerson = await AccountDAO.Instance.GetByID(report.ReportedPersonId);
                var checkExistedReporter = await AccountDAO.Instance.GetByID(report.ReporterId);
                if (checkExistedReportedPerson == null || checkExistedReporter == null) throw new Exception("Unexisted Id");
                
                //report.ReportedPerson = checkExistedReportedPerson;
                //report.Reporter = checkExistedReporter;
                await ReportDAO.Instance.AddReport(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Report> GetAll() => ReportDAO.Instance.GetAll();

        public async Task<Report?> GetReportById(int reporterId, int reportedId) => await ReportDAO.Instance.GetByID(reporterId, reportedId);

        public async Task RemoveReport(int reporterId, int reportedId)
        {
            try
            {
                var check = await ReportDAO.Instance.GetByID(reporterId, reportedId) ?? throw new Exception("Not found");
                await ReportDAO.Instance.RemoveReport(check);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedList<Report> Search(ReportSearchRequest searchRequest)
        {
            var query = ReportDAO.Instance.GetAll();
            // Apply search
            query = query.GetWithSearch(searchRequest).AsQueryable();
            // Apply sort
            //response.GetWithSort();
            // Apply pagination
            var pagingData = query.GetWithPaging(searchRequest);

            return pagingData;
        }

        public async Task UpdateReport(Report report)
        {
            try
            {
                var check = await ReportDAO.Instance.GetByID(report.ReporterId, report.ReportedPersonId) ?? throw new Exception("Not found");
                await ReportDAO.Instance.UpdateReport(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
