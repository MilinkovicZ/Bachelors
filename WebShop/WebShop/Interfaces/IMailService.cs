namespace WebShop.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(string header, string body, string to);
    }
}
