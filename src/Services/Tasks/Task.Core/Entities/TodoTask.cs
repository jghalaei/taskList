using GenericContracts.Common;

namespace Task.Core.Entities;

public class TodoTask : EntityBase
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Comment { get; set; } = "";
    public ETodoTaskStatus Status { get; set; }
    public TodoTask(Guid userId, string title, DateTime dueDate = default, string comment = "")
    {
        UserId = userId;
        Title = title;
        DueDate = dueDate;
        Comment = comment;
    }
}