using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Reporting.Entities;

namespace Reporting.Data;

public class ReportContext : IReportContext
{
    IConfiguration _configuration;
    public ReportContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
        string dbName = _configuration["DatabaseSettings:DatabaseName"] ?? "ReportingDb";
        var database = client.GetDatabase(dbName);
        Reports = database.GetCollection<Report>(_configuration["DatabaseSettings:CollectionName"]);
    }
    public IMongoCollection<Report> Reports { get; set; }

}