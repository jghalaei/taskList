using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reporting.Entities;

namespace Reporting.Endpoints
{
    public static class ReportingEndpoints
    {
        public static RouteGroupBuilder MapReportingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/reporting");
            group.MapGet("/users/", async (IRepository<Report> repository) => await GetUsersStats(repository));

            group.MapGet("/users/{userId}", async (IRepository<Report> repository, Guid userId, [FromQuery] DateTime? date) => await GetUsersStats(repository, userId, date));

            return group;
        }

        private static async Task<IResult> GetUsersStats(IRepository<Report> repository)
        {
            var result = await repository.GetAllAsync(report => report.ReportDate.Date == DateTime.Now.Date);
            return Results.Ok(result);
        }
        private static async Task<IResult> GetUsersStats(IRepository<Report> repository, Guid userId, DateTime? date)
        {
            var result = await repository.GetAllAsync(report => (report.UserID) == userId && (date == null && report.ReportDate.Date == DateTime.Now.Date));
            return Results.Ok(result);
        }

    }
}