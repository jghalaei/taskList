using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Common;

namespace Task.Application.Entities
{
    public class ToDoTask : EntityBase
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Comment { get; set; } = "";
        public ETodoTaskStaus Status { get; set; }
        public ToDoTask(Guid userId, string title, DateTime dueDate = default, string comment = "")
        {
            UserId = userId;
            Title = title;
            DueDate = dueDate;
            Comment = comment;
        }
    }

    public enum ETodoTaskStaus
    {
        Created,
        InProgress,
        Done,
        Canceled
    }
}