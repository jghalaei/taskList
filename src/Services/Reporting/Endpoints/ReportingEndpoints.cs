using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reporting.Entities;
using Reporting.Repositories;

namespace Reporting.Endpoints
{
    public static class ReportingEndpoints
    {
        public static RouteGroupBuilder MapReportingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/v1/report");
            group.MapGet("/", async (IReportRepository repository) => await GetUsersStats(repository));

            group.MapGet("/{userId}", async (IReportRepository repository, Guid userId, [FromQuery] DateTime? date) => await GetUsersStats(repository, userId, date));

            return group;
        }



        private static async Task<IResult> GetUsersStats(IRepository<Report> repository)
        {
            var result = await repository.GetAllAsync();
            return Results.Ok(result);
        }
        private static async Task<IResult> GetUsersStats(IRepository<Report> repository, Guid userId, DateTime? date)
        {
            var result = await repository.GetAllAsync(report => (report.UserID) == userId && (date == null && report.ReportDate.Date == DateTime.Now.Date));
            return Results.Ok(result);
        }

    }
}