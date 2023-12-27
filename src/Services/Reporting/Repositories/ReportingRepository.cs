using System.Linq.Expressions;
using GenericContracts.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using Reporting.Data;
using Reporting.Entities;

namespace Reporting.Repositories
{
    public class ReportingRepository : IReportRepository
    {
        private IReportContext _context;
        private ILogger<ReportingRepository> _logger;
        public ReportingRepository(IReportContext context, ILogger<ReportingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Reports.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetAllAsync(Expression<Func<Report, bool>> predicate)
        {
            return await _context.Reports.Find(predicate).ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(Guid Id)
        {
            return await _context.Reports.Find(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Report?> GetOneAsync(System.Linq.Expressions.Expression<Func<Report, bool>> predicate)
        {
            return await _context.Reports.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<Report> InsertAsync(Report entity)
        {
            entity.Id = Guid.NewGuid();
            await _context.Reports.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Report> UpdateAsync(Report entity)
        {
            var updateResult = await _context.Reports.ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                return entity;
            else
                return null;
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            FilterDefinition<Report> filter = Builders<Report>.Filter.Eq("Id", Id);
            var deleteResult = await _context.Reports.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Report> InsertUpdateAsync(Report entity)
        {
            var report = _context.Reports.Find(p => p.UserID == entity.UserID).FirstOrDefault();
            if (report == null)
                return await InsertAsync(entity);
            else
                return await UpdateAsync(entity);
        }
    }
}