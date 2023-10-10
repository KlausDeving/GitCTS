using System.Collections.Generic;

namespace CTS.Kanban;
public class KanbanColumn : IKanbanDomainModel
{
    public int Id { get; set; }
    public string StatusType { get; set; } = string.Empty;
    public virtual List<KanbanCard> KanbanCards { get; set; } = new();
}
