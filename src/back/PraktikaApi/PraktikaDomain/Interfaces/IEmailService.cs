namespace PraktikaDomain.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmCode(string mail, int code);
        Task SendTransportationStatus(string TransportationStatus , string mail);
    }
}
