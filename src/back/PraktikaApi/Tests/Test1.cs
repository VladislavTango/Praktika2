using AutoMapper;
using PraktikaApplication;
using PraktikaApplication.TransportationHandlers.Response;
using PraktikaDomain.Entities;
using PraktikaApplication.VehicleHandlers.Response;
using PraktikaDomain.Entities.TransportEntities;

namespace MapperProfileTests
{
    public class MapperProfileTests
    {
        private readonly IMapper _mapper;

        public MapperProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_Transportation_To_TransportationGetListResponse()
        {
            // Arrange
            var transportation = new Transportation
            {
                Id = 1,
                Order = new Order
                {
                    CargoDescription = "asd",
                    Client = new Client(),
                    ClientId = 1,
                    ContractList = new List<Contract>(),
                    CreatedDate = DateTime.Now,
                    Id =1,
                    OrderName = "das",
                    Status = true,
                    TransportationList = new List<Transportation>(),
                    OrderNumber = "12345"
                },
            };

            // Act
            var response = _mapper.Map<TransportationGetListResponse>(transportation);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(transportation.Order.OrderNumber,response.OrderNumber);
        }

        [Fact]
        public void Map_Vehicle_To_VehicleGetListResponse()
        {
            // Arrange
            var vehicle = new Vehicle();
            vehicle = new Vehicle
            {
                Id = 1,
                Status =true,
                Truck = new Truck
                {
                    Id = 2,
                    Mark = "123",
                    Number = "123",
                    Status = true,
                    VehicleId = 1,
                    Vehicle = vehicle
                },
                Trailer = new Trailer
                {
                    Id = 3,
                    Number = "123",
                    Status = true,
                    VehicleId = 1,
                    Vehicle = vehicle,
                    TrailerType = 0
                },
                TrailerId = 2,
                TruckId = 3
            };

            // Act
            var response = _mapper.Map<VehicleGetListResponse>(vehicle);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(vehicle.Truck.Id, response.Truck.Id);
            Assert.Equal(vehicle.Trailer.Id, response.Trailer.Id);
            Assert.Equal(vehicle.Trailer.TrailerType, response.Trailer.TrailerType);
        }
    }
}