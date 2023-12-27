using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Common;
using GenericContracts.EventBusMessages;

namespace Reporting.Entities
{
    public class Report : EntityBase
    {
        public Report(Guid userID, DateTime reportDate, DateTime dueDate)
        {
            this.UserID = userID;
            ReportDate = reportDate;
            TaskStats = new Dictionary<string, int>();
            DueDate = dueDate;
        }
        public Guid UserID { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime DueDate { get; set; }
        public Dictionary<string, int> TaskStats { get; set; }
    }
}