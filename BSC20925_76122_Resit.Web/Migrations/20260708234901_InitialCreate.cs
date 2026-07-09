using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSC20925_76122_Resit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceClaims_ClaimDate",
                table: "InsuranceClaims");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceClaims_CreatedAt",
                table: "InsuranceClaims");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InsuranceClaims");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "InsuranceClaims",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "System",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimType",
                table: "InsuranceClaims",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimStatus",
                table: "InsuranceClaims",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "Submitted",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 1);

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "Description", "UpdatedAt" },
                values: new object[] { "CLM-1001", "Submitted", "Motor", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9000), "mary@example.com", "Rear bumper damaged in minor accident", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimDate", "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "CustomerName", "Description", "IncidentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLM-1002", "Under Review", "Home", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9000), "john@example.com", "John Smith", "Water damage from burst pipe", new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimDate", "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "CustomerName", "Description", "EstimatedAmount", "IncidentDate", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLM-1003", "Approved", "Health", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9010), "sarah@example.com", "Sarah Johnson", "Hospital stay for surgery", 5000.00m, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.InsertData(
                table: "InsuranceClaims",
                columns: new[] { "Id", "ClaimDate", "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CreatedBy", "CustomerEmail", "CustomerName", "Description", "EstimatedAmount", "IncidentDate", "PolicyNumber", "UpdatedAt" },
                values: new object[] { 4, new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLM-1004", "Rejected", "Travel", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9020), "System", "michael@example.com", "Michael Brown", "Lost luggage during international flight", 750.00m, new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "POL-24680", new DateTime(2026, 7, 8, 23, 49, 1, 20, DateTimeKind.Utc).AddTicks(9020) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldDefaultValue: "System");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimType",
                table: "InsuranceClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "ClaimStatus",
                table: "InsuranceClaims",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldDefaultValue: "Submitted");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InsuranceClaims",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "Description", "UpdatedAt", "UserId" },
                values: new object[] { "CLM-2026-001", 1, 1, new DateTime(2026, 7, 8, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), "mary.obrien@example.com", "Rear bumper damaged in minor accident at supermarket parking lot", null, null });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClaimDate", "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "CustomerName", "Description", "IncidentDate", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLM-2026-002", 2, 2, new DateTime(2026, 7, 3, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), "john.murphy@example.com", "John Murphy", "Water damage to kitchen ceiling due to burst pipe", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 6, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), null });

            migrationBuilder.UpdateData(
                table: "InsuranceClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimDate", "ClaimReference", "ClaimStatus", "ClaimType", "CreatedAt", "CustomerEmail", "CustomerName", "Description", "EstimatedAmount", "IncidentDate", "UpdatedAt", "UserId" },
                values: new object[] { new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLM-2026-003", 4, 3, new DateTime(2026, 6, 28, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), "sarah.kelly@example.com", "Sarah Kelly", "Lost luggage during international flight with delayed delivery", 850.50m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 5, 17, 36, 58, 744, DateTimeKind.Utc).AddTicks(5400), null });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_ClaimDate",
                table: "InsuranceClaims",
                column: "ClaimDate");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_CreatedAt",
                table: "InsuranceClaims",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }
    }
}
