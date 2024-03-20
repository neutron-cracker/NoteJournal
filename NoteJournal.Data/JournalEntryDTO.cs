using System.Text.Json.Serialization;

namespace NoteJournal.Data;

public class JournalEntryDTO
{
    [JsonIgnore]
    public int Id { get; init; }
    public Guid GuidId { get; init; }
    public DateTime Created { get; init; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}