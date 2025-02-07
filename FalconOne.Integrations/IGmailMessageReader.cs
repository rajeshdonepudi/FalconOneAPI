using Chilkat;

namespace FalconOne.Integrations
{
    public interface IGmailMessageReader
    {
        void ChooseBox(string boxName);
        void Disconnect();
        Task<MemoryStream> DownloadAttachmentByIdAsync(string attachmentId);
        Task<Email> FetchSingleEmailAsync(uint uid);
        EmailBundle GetEmailBundle();
        Task<string> GetEmailFlags(uint messageId);
        void Login(string email, string password);
    }
}