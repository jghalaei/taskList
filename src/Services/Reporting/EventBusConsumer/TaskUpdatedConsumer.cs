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
    public class TaskUpdatedConsumer : IConsumer<TaskStatusUpdatedEvent>
    {
        private readonly IReportRepository _repository;

        public TaskUpdatedConsumer(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<TaskStatusUpdatedEvent> context)
        {
            var message = context.Message;
            Report report = await _repository.GetOneAsync(rpt => rpt.UserID == message.UserId) ?? new Report(message.UserId, message.CreationDate, message.DueDate);
            string oldStatusStr = message.OldStatus.ToString();
            string newStatusStr = message.NewStatus.ToString();
            if (!report.TaskStats.TryAdd(oldStatusStr, 0))
                report.TaskStats[oldStatusStr] -= 1;

            if (!report.TaskStats.TryAdd(newStatusStr, 1))
                report.TaskStats[newStatusStr] += 1;

            report.ReportDate = message.CreationDate;
            report.DueDate = message.DueDate;
            await _repository.InsertUpdateAsync(report);
        }
    }
}