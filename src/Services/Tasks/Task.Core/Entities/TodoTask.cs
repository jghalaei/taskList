using GenericContracts.Common;
using GenericContracts.EventBusMessages;

namespace Task.Core.Entities;

public class TodoTask : EntityBase
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Comment { get; set; } = "";
    public ETodoTaskStatus Status { get; set; }

}