namespace CapstoneTravelBlog.Settings
{
    public class MailSettings
    {
        public string SenderEmail { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Server { get; set; } = "";
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public bool RequiresAuthentication { get; set; }
    }
}
