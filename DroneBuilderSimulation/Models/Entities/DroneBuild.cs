using System;

namespace DroneBuilderSimulation.Models.Entities
{
    public class DroneBuild
    {
        public int Id { get; set; }
        
        public string BuildName { get; set; } = "My Custom Drone";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ============================================
        // Foreign Keys & Navigation Properties
        // ============================================

        // 1. Frame (Required)
        public int FrameId { get; set; }
        public Frame? Frame { get; set; }

        // 2. Motor (Required)
        public int MotorId { get; set; }
        public Motor? Motor { get; set; }

        // 3. Propeller (Required)
        public int PropellerId { get; set; }
        public Propeller? Propeller { get; set; }

        // 4. Flight Controller (Required)
        public int FlightControllerId { get; set; }
        public FlightController? FlightController { get; set; }

        // 5. Battery (Required)
        public int BatteryId { get; set; }
        public Battery? Battery { get; set; }

        // 6. VTX (Optional - notice the nullable int?)
        public int? VtxId { get; set; }
        public VideoTransmitter? Vtx { get; set; }

        // 7. Receiver (Optional - notice the nullable int?)
        public int? ReceiverId { get; set; }
        public RadioReceiver? Receiver { get; set; }

        // ============================================
        // Calculated Logic Properties 
        // (These store the results of your Physics Service)
        // ============================================
        
        public decimal TotalPrice { get; set; }
        
        public double TotalWeightGrams { get; set; }
        
        public double EstimatedFlightTimeMinutes { get; set; }
        
        public double EstimatedMaxSpeedKmh { get; set; }
    }
}