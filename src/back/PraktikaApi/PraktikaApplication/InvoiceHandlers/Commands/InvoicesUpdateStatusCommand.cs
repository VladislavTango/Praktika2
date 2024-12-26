using MediatR;

namespace PraktikaApplication.InvoicesHandlers.Commands
{
    public class InvoicesUpdateStatusCommand : IRequest
    {
        public int Id { get; set; }
        public bool Status {  get; set; } 
    }
}
