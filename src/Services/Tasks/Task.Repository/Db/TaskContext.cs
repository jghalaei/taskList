using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericContracts.Contracts;
using GenericTools.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task.Core.Entities;

namespace Task.Repository.Db
{
    public class TaskContext : GenericDbContext
    {
        public TaskContext(DbContextOptions options, IUserAccessor accessor) : base(options, accessor)
        {
        }

        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
    }
}