using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CTS.Kanban;
public class KanbanService<T> : IKanbanObjectService<T> where T : class, IKanbanDomainModel, new()
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _dbSet;
    public KanbanService(AppDbContext appDbContext)
    {
        _appDbContext=appDbContext;
        _dbSet=appDbContext.Set<T>();
    }
    public T Create(T kanban)
    {
        _dbSet.Add(kanban);
        _appDbContext.SaveChanges();
        return kanban;
    }

    public async Task<T> CreateAsync(T kanban)
    {
        _dbSet.Add(kanban);
        await _appDbContext.SaveChangesAsync();
        return kanban;
    }

    public async Task DeleteAsync(int id)
    {
        var kanban = await _dbSet.FindAsync(id)??throw new KanbanCardNotFoundException($"The object with id: {id} was not found");
        _dbSet.Remove(kanban);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        var kanban = await _dbSet.FindAsync(id);
        return kanban is null ? throw new KanbanCardNotFoundException($"The object with id: {id} was not found") : kanban;
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await Task.FromResult(_dbSet.AsEnumerable());
    }

    public async Task<T> UpsertAsync(T kanban)
    {
        var kanbanToUpdate = await _dbSet.FindAsync(kanban.Id);
        if(kanbanToUpdate is null)
        {
            kanbanToUpdate=new T();
            _dbSet.Add(kanbanToUpdate);
            await _appDbContext.SaveChangesAsync();
            return kanbanToUpdate;
        }
        await _appDbContext.SaveChangesAsync();
        return kanbanToUpdate;
    }
}

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(T service)
    {
        _services[typeof(T)]=service;
    }

    public static T GetService<T>()
    {
        return (T)_services[typeof(T)];
    }
}