namespace NoteJournal.Services;

public interface INoteContentEncryptor
{
    public Task<string> Encrypt(string clearText);
    public Task<string> Decrypt(string encryptedText);
}
public class NoteContentEncryptor : INoteContentEncryptor
{
    public Task<string> Encrypt(string clearText)
    {
        // TODO: implement
        return Task.FromResult(clearText);
    }

    public Task<string> Decrypt(string encryptedText)
    {
        // TODO: implement
        return Task.FromResult(encryptedText);
    }
}