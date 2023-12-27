using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericContracts.EventBusMessages
{
    public class TaskCreatedEvent : IntegrationBaseEvent
    {
        public TaskCreatedEvent(Guid userId, DateTime dueDate)
        {
            UserId = userId;
            DueDate = dueDate;
        }

        public Guid UserId { get; set; }

        public DateTime DueDate { get; set; }
    }
}