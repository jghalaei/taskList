using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using GenericContracts.EventBusMessages;
using MassTransit;
using Reporting.Entities;
using Reporting.Repositories;

namespace Reporting.EventBusConsumer
{
    public class TaskCreatedConsumer : IConsumer<TaskCreatedEvent>
    {
        IReportRepository _repository;
        ILogger<TaskCreatedConsumer> _logger;

        public TaskCreatedConsumer(IReportRepository repository, ILogger<TaskCreatedConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TaskCreatedEvent> context)
        {
            _logger.LogInformation("TaskCreatedEvent Message received: {@Message}", context.Message);
            var message = context.Message;
            Report report = await _repository.GetOneAsync(rpt => rpt.UserID == message.UserId) ?? new Report(
                message.UserId, message.CreationDate, message.DueDate);

            if (report.TaskStats.ContainsKey("Created"))
                report.TaskStats["Created"] += 1;
            else
                report.TaskStats.Add("Created", 1);
            report.ReportDate = message.CreationDate;
            report.DueDate = message.DueDate;
            await _repository.InsertUpdateAsync(report);

        }
    }
}