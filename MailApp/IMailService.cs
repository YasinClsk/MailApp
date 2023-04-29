namespace MailApp
{
    public interface IMailService
    {
        Task<bool> SendDiscountMessageAsync(MailModel mailModel);
        Task<bool> SendCustomMessageAsync(MailModel mailModel);
    }
}
