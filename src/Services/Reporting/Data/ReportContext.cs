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
        var database = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
        Reports = database.GetCollection<Report>(_configuration["DatabaseSettings:CollectionName"]);
    }
    public IMongoCollection<Report> Reports { get; set; }

}