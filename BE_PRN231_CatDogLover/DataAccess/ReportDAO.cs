using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<Report?> GetByID(int reporterId, int reportedId)
        {
            try
            {
                Report? query = null;
                query = await new PRN231Context().Reports.FirstOrDefaultAsync(u => u.ReportedPersonId == reportedId && u.ReporterId == reporterId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddReport(Report report)
        {
            try
            {
                var dbContext = new PRN231Context();
                var query = dbContext.Reports.Add(report).Entity;
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveReport(Report report)
        {
            try
            {
                var dbContext = new PRN231Context();
                var query = dbContext.Reports.Remove(report).Entity;
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Report> UpdateReport(Report report)
        {
            try
            {
                var dbContext = new PRN231Context();
                var query = dbContext.Reports.Update(report).Entity;
                await dbContext.SaveChangesAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Report> GetAll()
        {
            try
            {
                var query = new PRN231Context().Reports;
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
