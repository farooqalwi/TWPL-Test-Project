using System.ComponentModel.DataAnnotations;

namespace TWPL.Common.Model.Entities
{
    public class Vehicle
    {
        [Key]
        public long VehicleID { get; set; }

        [StringLength(50)]
        public string RegNo { get; set; }

        [StringLength(50)]
        public string Make { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string EngineNo { get; set; }

        [StringLength(50)]
        public string ChassisNo { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public bool Active { get; set; }
    }

}
