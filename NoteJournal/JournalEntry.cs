using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NoteJournal;

public class JournalEntryDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public Guid GuidId { get; set; }
    public DateTime Created { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}

public class JournalEntry
{
    public EntryContent Content { get; private set; }
    public string Name { get; private set; }
    public Identifier Identifier { get; }

    public JournalEntry(Identifier identifier, string name, string content)
    {
        Identifier = identifier;
        this.Content = new EntryContent(content);
        this.Name = name;
    }

    public void SetContent(string newContentText)
    {
        EntryContent newContent = new(newContentText);
        Content = newContent;
    }

    public void Rename(string newName)
    {
        Name = newName;
    }
}

public record Identifier(Guid Id);
public record EntryContent(string Content);