using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlanningAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "OperatorId", "ApiEndpoint", "Name" },
                values: new object[,]
                {
                    { 1, "api.htm.net", "HTM" },
                    { 2, "api.arriva.nl", "Arriva" }
                });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "LineId", "LinePlanningNumber", "OperatorNo" },
                values: new object[,]
                {
                    { 1, "Bus 24", 1 },
                    { 2, "Tram 12", 1 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "TripId", "ArrivalTime", "DepartureTime", "LineNo" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 2, 15, 45, 7, 360, DateTimeKind.Local).AddTicks(8067), new DateTime(2025, 6, 2, 14, 15, 7, 360, DateTimeKind.Local).AddTicks(8011), 2 },
                    { 2, new DateTime(2025, 6, 2, 16, 0, 7, 360, DateTimeKind.Local).AddTicks(8073), new DateTime(2025, 6, 2, 15, 30, 7, 360, DateTimeKind.Local).AddTicks(8071), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "LineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "OperatorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "LineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Operators",
                keyColumn: "OperatorId",
                keyValue: 1);
        }
    }
}
