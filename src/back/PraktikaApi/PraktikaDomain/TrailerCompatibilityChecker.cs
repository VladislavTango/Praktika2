using PraktikaDomain.Enums;
namespace PraktikaDomain
{
    public static class TrailerCompatibilityChecker
    {
        public static bool IsCombinationAllowed(CargoType cargoType, TrailerType trailerType) =>
            (cargoType, trailerType) switch
            {
                (CargoType.STANDART, TrailerType.DEFAULT) => true,

                (CargoType.DANGER, TrailerType.DEFAULT) => true,
                (CargoType.DANGER, TrailerType.BIG) => true,
                (CargoType.DANGER, TrailerType.LIQUID) => true,

                (CargoType.BIG, TrailerType.BIG) => true,

                (CargoType.LIQUID, TrailerType.LIQUID) => true,

                _ => false 
            };
    }
}
