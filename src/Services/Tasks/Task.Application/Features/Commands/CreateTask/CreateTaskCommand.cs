using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Task.Application.Features.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<Guid>
    {

        public CreateTaskCommand(Guid userId, string title, DateTime dueDate = default, string comment = "")
        {
            this.UserId = userId;
            this.Comment = comment;
            Title = title;
            DueDate = dueDate;
        }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime DueDate { get; set; }

    }
}