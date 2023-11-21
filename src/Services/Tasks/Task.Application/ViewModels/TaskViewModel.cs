using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Application.ViewModels;

public class TaskViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Comment { get; set; }
    public string Status { get; set; }
}