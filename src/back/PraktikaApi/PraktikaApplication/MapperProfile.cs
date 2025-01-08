using AutoMapper;
using PraktikaApplication.CargoHandlers.CargoCommand;
using PraktikaApplication.ClientHandlers.ClientCommand;
using PraktikaApplication.ContractHandlers.ContractHandler;
using PraktikaApplication.ContractHandlers.ContractResponse;
using PraktikaApplication.InovicesHandlers.Response;
using PraktikaApplication.OrderHandlers.OrderCommand;
using PraktikaApplication.OrderHandlers.Response;
using PraktikaApplication.TrailerHandlers.Response;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaApplication.TransportationHandlers.TransportationCommand;
using PraktikaApplication.TruckHandlers.Response;
using PraktikaApplication.VehicleHandlers.Response;
using PraktikaDomain.Entities;
using PraktikaDomain.Entities.TransportEntities;

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
            CreateMap<Transportation, TransportationGetListResponse>()
            .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order.OrderNumber));


            CreateMap<Cargo, CargoAddCommand>();
            CreateMap<Cargo, CargoUpdateCommand>();

            CreateMap<Invoices, InvoicesGetListResponse>();

            CreateMap<Truck, TruckGetListResponse>();

            CreateMap<Trailer, TrailerGetListResponse>();

            CreateMap<Vehicle, VehicleGetListResponse>()
                .ForMember(x => x.Truck, opt => opt.MapFrom(src => src.Truck))
                .ForMember(x => x.Trailer, opt => opt.MapFrom(src => src.Trailer));
        }
    }
}
