using DroneBuilderSimulation.Models.Entities;
using DroneBuilderSimulation.Services.Interfaces;

namespace DroneBuilderSimulation.Services.Implementations
{
    public class DronePhysicsService : IDronePhysicsService
    {
        public DroneBuild CalculateStats(DroneBuild build)
        {
            // 1. Calculate Total Weight
            // Start with frame weight
            double totalWeight = build.Frame?.WeightGrams ?? 0;

            // Add Motor weight (x4 for quadcopter)
            if (build.Motor != null)
            {
                totalWeight += build.Motor.WeightGrams * build.Motor.RequiredCount;
            }

            // Add Battery weight
            totalWeight += build.Battery?.WeightGrams ?? 0;

            // Add Propeller weight (Estimating 4 props)
            if (build.Propeller != null)
            {
                // Props are light, but let's assume 4x weight
                totalWeight += build.Propeller.WeightGrams * 4; 
            }

            // Add Stack and VTX
            totalWeight += build.FlightController?.WeightGrams ?? 0;
            totalWeight += build.Vtx?.WeightGrams ?? 0;
            totalWeight += build.Receiver?.WeightGrams ?? 0;

            build.TotalWeightGrams = totalWeight;

            // 2. Calculate Max Speed (Theoretical)
            // Formula: RPM = KV * Volts
            // Speed = RPM * Pitch * Constant
            if (build.Motor != null && build.Battery != null && build.Propeller != null)
            {
                // Voltage estimate: 3.7V per cell (Nominal) or 4.2V (Max). Let's use 3.7V nominal.
                double voltage = build.Battery.CellCountS * 3.7;
                
                double rpm = build.Motor.Kv * voltage;
                
                // Convert to km/h: (RPM * Pitch(inch) * 60 minutes) / (inches in a km)
                // Simplified constant: 0.001524 per blueprint
                double speedKmh = rpm * build.Propeller.PitchInch * 0.001524;
                
                // Apply a drag/efficiency factor (real world is slower)
                build.EstimatedMaxSpeedKmh = speedKmh * 0.85; 
            }

            // 3. Calculate Flight Time (Hover)
            // Formula: Time = (Capacity / Amps_Hover) * 60
            if (build.Battery != null && totalWeight > 0)
            {
                // Estimate Hover Amps: 
                // A generic drone needs ~150-200g thrust per 100g weight to hover? 
                // Actually, let's use a simplified energy density model.
                // Rule of thumb: Hover requires ~4-5 Watts per 100g of weight.
                
                double wattsPerGram = 0.05; // 5W per 100g = 0.05W/g
                double hoverWatts = totalWeight * wattsPerGram;
                
                double voltage = build.Battery.CellCountS * 3.7;
                double hoverAmps = hoverWatts / voltage;

                // Convert Capacity mAh to Ah
                double capacityAh = build.Battery.CapacityMah / 1000.0;

                if (hoverAmps > 0)
                {
                    build.EstimatedFlightTimeMinutes = (capacityAh / hoverAmps) * 60;
                }
            }
            
            // 4. Calculate Total Price
            decimal totalPrice = build.Frame?.Price ?? 0;
            totalPrice += (build.Motor?.Price ?? 0) * (build.Motor?.RequiredCount ?? 4);
            totalPrice += build.Battery?.Price ?? 0;
            totalPrice += build.FlightController?.Price ?? 0;
            totalPrice += build.FlightController?.Price ?? 0;
            totalPrice += build.Vtx?.Price ?? 0;
            totalPrice += build.Receiver?.Price ?? 0;
            totalPrice += build.Propeller?.Price ?? 0;
            
            
            build.TotalPrice = totalPrice;

            return build;
        }
    }
}