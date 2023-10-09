namespace CTS.Kanban;
public class KanbanCard : IKanbanDomainModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StatusType { get; set; } = string.Empty;
}
