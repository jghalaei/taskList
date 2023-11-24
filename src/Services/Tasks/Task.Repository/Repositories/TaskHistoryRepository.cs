using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericTools.Repositories;
using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;
using Task.Repository.Db;

namespace Task.Repository.Repositories
{
    public class TaskHistoryRepository : GenericRepository<TaskHistory>
    {
        public TaskHistoryRepository(TaskContext dbContext) : base(dbContext)
        {
        }
    }
}