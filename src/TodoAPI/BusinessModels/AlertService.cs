namespace TodoAPI.BusinessModels
{
    public class AlertService : IAlertService
    {
        public void SendEmailAlert(string email, string alert)
        {
            System.Console.WriteLine("Generated log by di");
        }
    }
}

