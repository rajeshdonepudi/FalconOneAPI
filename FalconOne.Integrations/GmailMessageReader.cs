using Chilkat;
using System.Diagnostics;

namespace FalconOne.Integrations
{
    public class GmailMessageReader : IGmailMessageReader
    {
        private readonly Global _glob;

        private readonly Imap _imap;

        public GmailMessageReader()
        {
            _glob = new Global();

            var unlocked = _glob.UnlockBundle("Anything for 30-day trial");

            _imap = new Imap();

            if (!unlocked)
            {
                Debug.WriteLine(_glob.LastErrorText);
            }

            if (unlocked)
            {
                Debug.WriteLine("Unlocked in trial mode.");
            }

            SetBaseConfig();
            Connect();
        }

        private void SetBaseConfig()
        {
            _imap.Ssl = true;
            _imap.Port = 993;
            _imap.VerboseLogging = true;
        }

        private void Connect()
        {
            var connectionSuccess = _imap.Connect("imap.gmail.com");

            Debug.WriteLine($"Connected: {connectionSuccess}");
        }

        public void Login(string email, string password)
        {
            var loginSuccess = _imap.Login(email, password);

            var rr = _imap.LastErrorText;

            Debug.WriteLine($"Login Status: {loginSuccess}");
        }

        public (uint EmailUid, int AttachmentIndex) ParseAttachmentId(string attachmentId)
        {
            var parts = attachmentId.Split('-');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid attachment ID format.");
            }

            uint emailUid = uint.Parse(parts[0]);
            int attachmentIndex = int.Parse(parts[1]);

            return (emailUid, attachmentIndex);
        }

        public async Task<MemoryStream> DownloadAttachmentByIdAsync(string attachmentId)
        {
            var (emailUid, attachmentIndex) = ParseAttachmentId(attachmentId);
            var task = _imap.FetchSingleAsync(emailUid, true);

            if (task == null)
            {
                throw new Exception("Failed to create Chilkat.Task for fetching the email.");
            }

            bool started = task.Run();

            if (!started)
            {
                throw new Exception($"Failed to start the task: {task.LastErrorText}");
            }

            while (!task.Finished)
            {
                await System.Threading.Tasks.Task.Delay(100);
            }

            if (!task.TaskSuccess)
            {
                throw new Exception($"Error fetching email: {task.LastErrorText}");
            }

            var email = new Chilkat.Email();

            bool success = email.LoadTaskResult(task);

            if (!success)
            {
                throw new Exception("Failed to load email from task result.");
            }

            if (attachmentIndex >= email.NumAttachments)
            {
                throw new ArgumentOutOfRangeException($"Attachment index {attachmentIndex} is out of range.");
            }

            byte[] attachmentData = email.GetAttachmentData(attachmentIndex);

            var memoryStream = new MemoryStream(attachmentData);

            return memoryStream;
        }


        public async Task<Email> FetchSingleEmailAsync(uint uid)
        {
            var task = _imap.FetchSingleAsync(uid, true);

            if (task == null)
            {
                throw new Exception("Failed to create Chilkat.Task for fetching the email.");
            }

            bool started = task.Run();

            if (!started)
            {
                throw new Exception($"Failed to start the task: {task.LastErrorText}");
            }

            while (!task.Finished)
            {
                await System.Threading.Tasks.Task.Delay(100);
            }

            if (!task.TaskSuccess)
            {
                throw new Exception($"Error fetching email: {task.LastErrorText}");
            }

            var email = new Chilkat.Email();

            bool success = email.LoadTaskResult(task);

            if (!success)
            {
                throw new Exception("Failed to load email from task result.");
            }
            return email;
        }

        public async Task<string> GetEmailFlags(uint messageId)
        {
            var result = await System.Threading.Tasks.Task.Run(() => _imap.FetchFlags(messageId, true));

            return result;
        }

        public void ChooseBox(string boxName)
        {
            var selectionSuccess = _imap.SelectMailbox(boxName);

            Debug.WriteLine($"Box selection Status: {selectionSuccess}");
        }

        public EmailBundle GetEmailBundle()
        {
            var messageSet = _imap.Search($"SINCE {DateTime.UtcNow.AddDays(-1).ToString(@"dd-MMM-yyyy")}", true);

            if (messageSet == null)
            {
                Console.WriteLine(_imap.LastErrorText);
            }

            var emailBundle = _imap.FetchBundle(messageSet);

            if (emailBundle == null)
            {
                Console.WriteLine(_imap.LastErrorText);
            }

            return emailBundle;
        }

        public void Disconnect()
        {
            _imap.Disconnect();
        }
    }
}
