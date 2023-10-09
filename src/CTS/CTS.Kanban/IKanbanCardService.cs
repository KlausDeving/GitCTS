using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTS.Kanban;



public interface IKanbanDomainModel
{
    int Id { get; set; }
}


public interface IKanbanObjectService<T> where T : IKanbanDomainModel
{
    T Create(T kanbanCard);
    Task<T> CreateAsync(T kanbanCard);
    Task DeleteAsync(int id);
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAsync();
    Task<T> UpsertAsync(T kanbanCard);
}