namespace TelecomLayer
{
    public class TelecomSettings
    {
        public EmailSettings EmailSettings { get; set; }
    }

    public class EmailSettings
    {
        public string SenderName { get; set; }
        public string SenderSecret { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    //public class SMSSettings
    //{
    //    public string SenderName { get; set; }
    //    public string SenderSecret { get; set; }
    //    public string Host { get; set; }
    //    public int Port { get; set; }
    //}
}
