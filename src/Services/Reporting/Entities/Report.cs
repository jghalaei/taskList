using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Common;

namespace Reporting.Entities
{
    public class Report : EntityBase
    {
        public Report(Guid userID, DateTime reportDate, Dictionary<string, int> taskStats)
        {
            this.UserID = userID;
            ReportDate = reportDate;
            TaskStats = taskStats;
        }
        public Guid UserID { get; set; }
        public DateTime ReportDate { get; set; }
        public Dictionary<string, int> TaskStats { get; set; }
    }
}