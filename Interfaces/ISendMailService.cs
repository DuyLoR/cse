namespace CSE_website.Interfaces
{
    public interface ISendMailService 
    {
        void SendEmail(string to, string subject, string body);
    }
}