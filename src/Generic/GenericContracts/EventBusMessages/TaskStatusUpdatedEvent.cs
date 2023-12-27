namespace GenericContracts.EventBusMessages;

public class TaskStatusUpdatedEvent : IntegrationBaseEvent
{
    public TaskStatusUpdatedEvent(Guid userId, DateTime dueDate, ETodoTaskStatus oldStatus, ETodoTaskStatus newStatus)
    {
        UserId = userId;
        DueDate = dueDate;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }

    public Guid UserId { get; set; }
    public DateTime DueDate { get; set; }
    public ETodoTaskStatus OldStatus { get; set; }
    public ETodoTaskStatus NewStatus { get; set; }
}