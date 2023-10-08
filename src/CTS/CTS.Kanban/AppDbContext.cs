using Microsoft.EntityFrameworkCore;

namespace CTS.Kanban;
public class AppDbContext : DbContext
{
    public DbSet<KanbanCard> KanbanCards { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=16.171.170.18;Database=ctsdatabase;Username=ccs;Password=23786680;SSL Mode=Require;Trust Server Certificate=True;");
    }
}
