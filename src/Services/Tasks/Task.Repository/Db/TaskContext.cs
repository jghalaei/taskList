using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Core.Entities;

namespace Task.Repository.Db
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {

        }
        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
    }
}