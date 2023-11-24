using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Application.ViewModels
{
    public class TaskHistoryViewModel
    {
        public Guid TaskId { get; set; }
        public DateTime Date { get; set; }
        public ETodoTaskStatus OldStatus { get; set; }
        public ETodoTaskStatus NewStatus { get; set; }
        public string Comment { get; set; } = "";
    }
}