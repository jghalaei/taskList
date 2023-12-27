using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using Reporting.Entities;

namespace Reporting.Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<Report> InsertUpdateAsync(Report entity);
    }
}