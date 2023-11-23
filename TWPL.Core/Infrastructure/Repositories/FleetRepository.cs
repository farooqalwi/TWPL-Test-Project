using Microsoft.EntityFrameworkCore;
using TWPL.Common.Model.Entities;
using TWPL.Core.Infrastructure.Context;
using TWPL.Core.Infrastructure.Interfaces.IRepositories;

namespace TWPL.Core.Infrastructure.Repositories
{
    public class FleetRepository : IFleetRepository
    {
        private readonly FleetDbContext _dbContext;
        public FleetRepository(FleetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteVehicle(long vehicleId)
        {
            var vehicle = await _dbContext.Vehicle.FindAsync(vehicleId);
            if (vehicle == null)
            {
                return false;
            }

            _dbContext.Vehicle.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Vehicle>> GetVehiclesList()
        {
            return await _dbContext.Vehicle.ToListAsync();
        }

        public async Task<bool> AddVehicle(Vehicle newVehicle)
        {
            try
            {
                _dbContext.Vehicle.Add(newVehicle);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddVehicles(List<Vehicle> vehicles)
        {
            try
            {
                if (vehicles.Any())
                {
                    foreach (var vehicle in vehicles)
                    {
                        try
                        {
                            _dbContext.Vehicle.Add(vehicle);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateVehicle(Vehicle vehicle)
        {
            var dbVehicle = await _dbContext.Vehicle.FindAsync(vehicle.VehicleID);
            if (dbVehicle == null)
            {
                return false;
            }

            dbVehicle.RegNo = vehicle.RegNo;
            dbVehicle.Make = vehicle.Make;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.Color = vehicle.Color;
            dbVehicle.EngineNo = vehicle.EngineNo;
            dbVehicle.ChassisNo = vehicle.ChassisNo;
            dbVehicle.DateOfPurchase = vehicle.DateOfPurchase;
            dbVehicle.Active = vehicle.Active;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
