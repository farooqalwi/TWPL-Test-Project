using TWPL.Common.Model.Entities;

namespace TWPL.Core.Infrastructure.Interfaces.IRepositories
{
    public interface IFleetRepository
    {
        Task<IList<Vehicle>> GetVehiclesList();
        Task<bool> DeleteVehicle(long vehicleId);
        Task<bool> UpdateVehicle(Vehicle vehicle);
        Task<bool> AddVehicle(Vehicle vehicle);
        Task<bool> AddVehicles(List<Vehicle> vehicles);
    }
}
