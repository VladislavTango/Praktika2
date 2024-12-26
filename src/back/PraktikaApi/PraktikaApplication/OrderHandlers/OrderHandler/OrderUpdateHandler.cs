using AutoMapper;
using CommonShared.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaDataPersistance.ApplicationContext;

namespace PraktikaApplication.OrderHandlers.OrderHandler
{
    public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderUpdateHandler(AppDbContext appDbContext , IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }

        public async Task Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
                throw new Exception("Order not found");

            _mapper.Map(request, order);

            order.Status = request.Status;

            await _context.SaveChangesAsync();
        }

    }
}
