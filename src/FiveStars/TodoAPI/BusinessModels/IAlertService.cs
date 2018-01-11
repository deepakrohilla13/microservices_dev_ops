namespace TodoAPI.BusinessModels
{
    public interface IAlertService
    {
        void SendEmailAlert(string email, string alert);
    }
}