using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.OrderHandlers.OrderHandler
{
    public class OrderDeleteHandler : IRequestHandler<OrderDeleteCommand,int>
    {
        private readonly AppDbContext _context;

        public OrderDeleteHandler(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
        public async Task<int> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (order == null)
                throw new Exception("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return request.Id;
        }
    }
}
