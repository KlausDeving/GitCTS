using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS.Kanban;
public class KanbanCardService : IKanbanObjectService<KanbanCard>
{
    private readonly AppDbContext _appDbContext;

    public KanbanCardService(AppDbContext appDbContext)
    {
        _appDbContext=appDbContext;
    }
    public KanbanCard Create(KanbanCard kanbanCard)
    {
        _appDbContext.KanbanCards.Add(kanbanCard);
        _appDbContext.SaveChanges();
        return kanbanCard;
    }

    public async Task<KanbanCard> CreateAsync(KanbanCard kanbanCard)
    {
        _appDbContext.KanbanCards.Add(kanbanCard);
        await _appDbContext.SaveChangesAsync();
        return kanbanCard;
    }

    public async Task DeleteAsync(int id)
    {
        var kanbanCard = await _appDbContext.KanbanCards.FindAsync(id)??throw new KanbanCardNotFoundException($"Card with Id: {id} not found");
        _appDbContext.KanbanCards.Remove(kanbanCard);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<KanbanCard> GetAsync(int id)
    {
        var kanbanCard = await _appDbContext.KanbanCards.FindAsync(id);
        return kanbanCard is null ? throw new KanbanCardNotFoundException($"Card with Id: {id} not found") : kanbanCard;
    }

    public async Task<IEnumerable<KanbanCard>> GetAsync()
    {
        return await Task.FromResult(_appDbContext.KanbanCards.AsEnumerable());
    }

    public async Task<KanbanCard> UpsertAsync(KanbanCard kanbanCard)
    {

        var kanbanCardToUpdate = await _appDbContext.KanbanCards.FindAsync(kanbanCard.Id);
        if(kanbanCardToUpdate is null)
        {
            kanbanCardToUpdate=new KanbanCard()
            {
                Title=kanbanCard.Title,
                Description=kanbanCard.Description,
                StatusType=kanbanCard.StatusType
            };
            _appDbContext.KanbanCards.Add(kanbanCardToUpdate);
            await _appDbContext.SaveChangesAsync();
            return kanbanCardToUpdate;
        }
        kanbanCardToUpdate.Title=kanbanCard.Title;
        kanbanCardToUpdate.Description=kanbanCard.Description;
        kanbanCardToUpdate.StatusType=kanbanCard.StatusType;
        await _appDbContext.SaveChangesAsync();
        return kanbanCardToUpdate;
    }

}
