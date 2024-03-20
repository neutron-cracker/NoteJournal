using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NoteJournal.Data;

public class JournalDbContext : DbContext
{
    public DbSet<JournalEntryDTO> JournalEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JournalEntryDTO>(builder =>
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Created)
                // ReSharper disable once StringLiteralTypo
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.GuidId)
                .HasValueGenerator(typeof(SequentialGuidValueGenerator));
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(options =>
        {
            options.MigrationsHistoryTable("Journal.__MigrationHistory");
        });
        base.OnConfiguring(optionsBuilder);
    }
}