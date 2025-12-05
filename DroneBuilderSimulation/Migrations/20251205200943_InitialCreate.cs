using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneBuilderSimulation.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CellCountS = table.Column<int>(type: "INTEGER", nullable: false),
                    CapacityMah = table.Column<int>(type: "INTEGER", nullable: false),
                    CRating = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightControllers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Processor = table.Column<string>(type: "TEXT", nullable: false),
                    EscCurrentRatingAmps = table.Column<int>(type: "INTEGER", nullable: false),
                    Firmware = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightControllers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SizeCategory = table.Column<string>(type: "TEXT", nullable: false),
                    Material = table.Column<string>(type: "TEXT", nullable: false),
                    WheelbaseMm = table.Column<double>(type: "REAL", nullable: false),
                    MaxPropSizeInch = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kv = table.Column<int>(type: "INTEGER", nullable: false),
                    StatorSize = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxContinuousCurrentAmps = table.Column<double>(type: "REAL", nullable: false),
                    MaxPowerWatts = table.Column<double>(type: "REAL", nullable: false),
                    RequiredCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiameterInch = table.Column<double>(type: "REAL", nullable: false),
                    PitchInch = table.Column<double>(type: "REAL", nullable: false),
                    BladeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RadioReceivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Protocol = table.Column<string>(type: "TEXT", nullable: false),
                    FrequencyGhz = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadioReceivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoTransmitters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PowerMw = table.Column<double>(type: "REAL", nullable: false),
                    MaxRangeKm = table.Column<double>(type: "REAL", nullable: false),
                    IsDigital = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTransmitters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DroneBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuildName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FrameId = table.Column<int>(type: "INTEGER", nullable: false),
                    MotorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PropellerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FlightControllerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BatteryId = table.Column<int>(type: "INTEGER", nullable: false),
                    VtxId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReceiverId = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWeightGrams = table.Column<double>(type: "REAL", nullable: false),
                    EstimatedFlightTimeMinutes = table.Column<double>(type: "REAL", nullable: false),
                    EstimatedMaxSpeedKmh = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneBuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_Batteries_BatteryId",
                        column: x => x.BatteryId,
                        principalTable: "Batteries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_FlightControllers_FlightControllerId",
                        column: x => x.FlightControllerId,
                        principalTable: "FlightControllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_Frames_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Frames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_Motors_MotorId",
                        column: x => x.MotorId,
                        principalTable: "Motors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_Propellers_PropellerId",
                        column: x => x.PropellerId,
                        principalTable: "Propellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneBuilds_RadioReceivers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "RadioReceivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DroneBuilds_VideoTransmitters_VtxId",
                        column: x => x.VtxId,
                        principalTable: "VideoTransmitters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_BatteryId",
                table: "DroneBuilds",
                column: "BatteryId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_FlightControllerId",
                table: "DroneBuilds",
                column: "FlightControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_FrameId",
                table: "DroneBuilds",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_MotorId",
                table: "DroneBuilds",
                column: "MotorId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_PropellerId",
                table: "DroneBuilds",
                column: "PropellerId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_ReceiverId",
                table: "DroneBuilds",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_DroneBuilds_VtxId",
                table: "DroneBuilds",
                column: "VtxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroneBuilds");

            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "FlightControllers");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Motors");

            migrationBuilder.DropTable(
                name: "Propellers");

            migrationBuilder.DropTable(
                name: "RadioReceivers");

            migrationBuilder.DropTable(
                name: "VideoTransmitters");
        }
    }
}
