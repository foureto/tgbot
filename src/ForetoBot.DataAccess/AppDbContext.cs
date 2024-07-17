using ForetoBot.DataAccess.Domain.Games;
using ForetoBot.DataAccess.Domain.References;
using Microsoft.EntityFrameworkCore;

namespace ForetoBot.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DomanCategory> DomanCategories { get; set; }
    public DbSet<DomanCard> DomanCards { get; set; }

    public DbSet<StoredText> Texts { get; set; }
    public DbSet<StoredFile> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DomanCategory>(entity =>
        {
            entity.Navigation(e => e.Name).AutoInclude();
            entity.Navigation(e => e.Description).AutoInclude();
            entity.Navigation(e => e.Label).AutoInclude();
            entity.Navigation(e => e.Cards).AutoInclude();
        });

        modelBuilder.Entity<DomanCard>(entity =>
        {
            entity.Navigation(e => e.Title).AutoInclude();
            entity.Navigation(e => e.Image).AutoInclude();
            entity.Navigation(e => e.TitleSound).AutoInclude();
            entity.Navigation(e => e.DescriptionSound).AutoInclude();
        });
    }
}