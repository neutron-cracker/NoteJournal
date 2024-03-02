using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NoteJournal;

public class JournalDbContext : DbContext
{
    public DbSet<JournalEntryDTO> JournalEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JournalEntryDTO>(builder =>
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Created)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.GuidId)
                .HasValueGenerator(typeof(SequentialGuidValueGenerator));
        });

        base.OnModelCreating(modelBuilder);
    }
}