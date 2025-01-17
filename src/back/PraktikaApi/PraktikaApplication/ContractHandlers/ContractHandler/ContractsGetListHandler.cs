﻿using AutoMapper;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaApplication.ContractHandlers.ContractResponse;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaDataPersistance.ApplicationContext;
using PraktikaDomain.Entities;
using PraktikaDomain.Interfaces;


namespace PraktikaApplication.ContractHandlers.ContractCommand
{
    public class ContractsGetListHandler : IRequestHandler<ContractGetListCommand, List<ContractGetListResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ContractsGetListHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContractGetListResponse>> Handle(ContractGetListCommand request, CancellationToken cancellationToken)
        {
            var gridifyQuery = new GridifyQuery()
            {
                OrderBy = request.SortBy,
            };

            var contracts = await _context.Contracts
             .Include(c => c.Order)
             .ApplyFiltering(gridifyQuery)
             .ApplyOrdering(gridifyQuery)
             .Skip((request.PageNumber - 1) * request.PageLength)
             .Take(request.PageLength)
             .ToListAsync(cancellationToken);

            return _mapper.Map<List<ContractGetListResponse>>(contracts);
        }
    }
}
