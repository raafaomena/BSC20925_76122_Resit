using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSC20925_76122_Resit.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 8, 13, 17, 58, 378, DateTimeKind.Utc).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 3, 13, 17, 58, 378, DateTimeKind.Utc).AddTicks(5680), new DateTime(2026, 7, 6, 13, 17, 58, 378, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 28, 13, 17, 58, 378, DateTimeKind.Utc).AddTicks(5680), new DateTime(2026, 7, 5, 13, 17, 58, 378, DateTimeKind.Utc).AddTicks(5680) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 7, 8, 13, 17, 48, 426, DateTimeKind.Utc).AddTicks(2230));

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 3, 13, 17, 48, 426, DateTimeKind.Utc).AddTicks(2230), new DateTime(2026, 7, 6, 13, 17, 48, 426, DateTimeKind.Utc).AddTicks(2230) });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 28, 13, 17, 48, 426, DateTimeKind.Utc).AddTicks(2230), new DateTime(2026, 7, 5, 13, 17, 48, 426, DateTimeKind.Utc).AddTicks(2230) });
        }
    }
}
