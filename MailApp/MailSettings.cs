namespace MailApp
{
    public class MailSettings
    {
        public String From { get; set; }
        public String Password { get; set; }
        public int SMTPPort { get; set; }
        public bool SMTPEnableSsl { get; set; }
        public String SMTPHost { get; set; }
        public String DisplayName { get; set; }
    }

}