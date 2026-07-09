using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSC20925_76122_Resit.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToInsuranceClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2026, 7, 8, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), null });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2026, 7, 3, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), new DateTime(2026, 7, 6, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), null });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2026, 6, 28, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), new DateTime(2026, 7, 5, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InsuranceClaims");

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
    }
}
