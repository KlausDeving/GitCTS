using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTS.Kanban;

public interface IKanbanCardService
{
    KanbanCard Create(KanbanCard kanbanCard);
    Task<KanbanCard> CreateAsync(KanbanCard kanbanCard);
    Task DeleteAsync(int id);
    Task<KanbanCard> GetAsync(int id);
    Task<IEnumerable<KanbanCard>> GetAsync();
    Task<KanbanCard> UpsertAsync(KanbanCard kanbanCard);
}