using Microsoft.EntityFrameworkCore;

namespace CTS.Kanban;
public class AppDbContext : DbContext
{
    public DbSet<KanbanCard> KanbanCards { get; set; }
    public DbSet<KanbanColumn> KanbanColumns { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=16.171.4.83;Database=ctsdatabase;Username=ccs;Password=23786680;SSL Mode=Require;Trust Server Certificate=True;");
    }
}
