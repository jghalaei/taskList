using MongoDB.Driver;
using Reporting.Entities;

namespace Reporting.Data;

public interface IReportContext
{
    IMongoCollection<Report> Reports { get; }
}
