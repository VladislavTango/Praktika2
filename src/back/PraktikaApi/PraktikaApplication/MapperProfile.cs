using AutoMapper;
using PraktikaApplication.CargoHandlers.CargoCommand;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaApplication.ContractHandlers.ContractResponse;
using PraktikaApplication.InovicesHandlers.Response;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaDomain.Entities;

namespace PraktikaApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<ClientUpdateCommand, Client>();
            CreateMap<ClientAddCommand, Client>();

            CreateMap<OrderAddCommand, Order>();
            CreateMap<OrderUpdateCommand, Order>();
            CreateMap<Order, OrderGetListResponse>();

            CreateMap<ContractAddCommand, Contract>();
            CreateMap<ContractUpdateCommand, Contract>();
            CreateMap<Contract, ContractGetListResponse>();

            CreateMap<TransportationUpdateCommand, Transportation>();
            CreateMap<TransportationAddCommand, Transportation>();
            CreateMap<Transportation, TransportationGetListResponse>();


            CreateMap<Cargo, CargoAddCommand>();
            CreateMap<Cargo, CargoUpdateCommand>();

            CreateMap<Invoices, InvoicesGetListResponse>();
        }
    }
}
