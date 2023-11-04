using GenericContracts.Common;

namespace Task.Core.Entities;

public class TaskHistory : EntityBase
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public ETodoTaskStatus OldStatus { get; set; }
    public ETodoTaskStatus NewStatus { get; set; }
    public string Comment { get; set; } = "";

}