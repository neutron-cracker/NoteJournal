using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteJournal.Data;
using NoteJournal.Services;

namespace NoteJournal;

[ApiController]
[Route("api/entries")]
public class EntryController(JournalDbContext dbContext, INoteContentEncryptor encryptor) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var items = await dbContext.JournalEntries
            .AsNoTracking()
            .AsAsyncEnumerable()
            .SelectAwait(async note =>
            {
                if (note.Content is not null)
                {
                    note.Content = await encryptor.Decrypt(note.Content);
                }

                return note;
            })
            .ToListAsync();
        
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEntry([FromBody] JournalEntryDTO entry)
    {
        var existingItem = await dbContext.JournalEntries
            .FirstOrDefaultAsync(x => x.GuidId == entry.GuidId);

        entry.Content = entry.Content == null
            ? null
            : await encryptor.Encrypt(entry.Content); 
        
        if (existingItem == null)
        {
            dbContext.Add(entry);
        }
        else
        {
            existingItem.Content = entry.Content;
            existingItem.Name = entry.Name;
        }

        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await dbContext.JournalEntries.FirstOrDefaultAsync(x => x.GuidId == id);
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }
}