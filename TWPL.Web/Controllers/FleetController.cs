using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Net.Http.Headers;
using TWPL.Common.Model.Entities;
using TWPL.Core.Infrastructure.Interfaces.IRepositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TWPL.Web.Controllers
{
    public class FleetController : Controller
    {
        private readonly ILogger<FleetController> _logger;
        private readonly IFleetRepository _fleetRepository;

        public FleetController(ILogger<FleetController> logger, IFleetRepository fleetRepository)
        {
            _logger = logger;
            _fleetRepository = fleetRepository;
        }

        [Route("fleet")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _fleetRepository.GetVehiclesList();
                return View(data);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(new List<Vehicle>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(long vehicleId)
        {
            try
            {
                var isDeleted = await _fleetRepository.DeleteVehicle(vehicleId);
                if (isDeleted)
                {
                    return Json(new { success = true, message = "Vehicle deleted successfully" });
                }

                return Json(new { success = false, message = "Vehicle not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message =  ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle([FromBody]Vehicle vehicle)
        {
            try
            {
                var isUpdated = await _fleetRepository.UpdateVehicle(vehicle);
                if (isUpdated)
                {
                    return Json(new { success = true, message = "Vehicle updated successfully" });
                }

                return Json(new { success = false, message = "Unable to update vehicle" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                var isAdded = await _fleetRepository.AddVehicle(vehicle);
                if (isAdded)
                {
                    return Json(new { success = true, message = "Vehicle added successfully" });
                }

                return Json(new { success = false, message = "Unable to add vehicle" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicles([FromBody] List<Vehicle> vehicles)
        {
            try
            {
                var isAdded = await _fleetRepository.AddVehicles(vehicles);
                if (isAdded)
                {
                    return Json(new { success = true, message = "Vehicles added successfully" });
                }

                return Json(new { success = false, message = "Unable to add vehicles" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> ExportVehiclesDataToExcel()
        {
            try
            {
                // Get the list of vehicles from the database
                var vehicles = await _fleetRepository.GetVehiclesList();

                // Create a new Excel package
                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet
                    var worksheet = package.Workbook.Worksheets.Add("Vehicles");

                    // Set the header row
                    worksheet.Cells[1, 1].Value = "Vehicle ID";
                    worksheet.Cells[1, 2].Value = "Reg No";
                    worksheet.Cells[1, 3].Value = "Make";
                    worksheet.Cells[1, 4].Value = "Model";
                    worksheet.Cells[1, 5].Value = "Color";
                    worksheet.Cells[1, 6].Value = "Engine No";
                    worksheet.Cells[1, 7].Value = "Chassis No";
                    worksheet.Cells[1, 8].Value = "Date Of Purchase";
                    worksheet.Cells[1, 9].Value = "Active";

                    // Populate the data rows
                    for (var row = 2; row <= vehicles.Count + 1; row++)
                    {
                        var vehicle = vehicles[row - 2];
                        worksheet.Cells[row, 1].Value = vehicle.VehicleID;
                        worksheet.Cells[row, 2].Value = vehicle.RegNo;
                        worksheet.Cells[row, 3].Value = vehicle.Make;
                        worksheet.Cells[row, 4].Value = vehicle.Model;
                        worksheet.Cells[row, 5].Value = vehicle.Color;
                        worksheet.Cells[row, 6].Value = vehicle.EngineNo;
                        worksheet.Cells[row, 7].Value = vehicle.ChassisNo;
                        worksheet.Cells[row, 8].Value = vehicle.DateOfPurchase.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 9].Value = vehicle.Active ? "Yes" : "No";
                    }

                    // Auto-fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Set content type and return the file
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = $"Vehicles_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    var contentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };
                    Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                    Response.Headers.Add("Content-Type", contentType);

                    return File(package.GetAsByteArray(), contentType, fileName);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}