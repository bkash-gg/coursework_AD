using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cbbf4ada-0e20-42cb-92ff-fbedcb019c7a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0063b53-5452-4f50-9628-0b0fef5013c4"));

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupNotes",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "c72125e3-8803-453d-8f5e-f01d42b2c25a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "0908aadd-2271-40eb-bf04-8d368308b145");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "67083f48-3018-4899-840c-36a1d8432d1a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiry", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b92c49d3-2503-4c04-abdf-205040d3ca23"), 0, "Kathmandu, Nepal", "6528a40a-4810-4e11-a6ef-7f640ca7d932", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAEKD20aReiS8+8JgB2AQQ4i+zNmqLfziQ+4QZsOCqn2z/XpgnnVLW5OOdtdZpL5Q+QA==", "1234567890", true, null, null, new DateTime(2025, 5, 19, 13, 26, 14, 537, DateTimeKind.Utc).AddTicks(9269), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "fd4ec7d4-2e0e-41b4-8884-f3ac4f012997", 0, false, "admin@bookstore" },
                    { new Guid("be2ebb32-a092-4c1e-a764-3adffd28d83f"), 0, "Kathmandu, Nepal", "5a8ae987-e059-4551-a6c5-f658129b6b4e", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAEJ/JzjJU88e5luNOpRIQuN/SgjpfTjvruHWUsojhsFkQFGCa98QIJ4zblHx9V/dTIQ==", "0987654321", true, null, null, new DateTime(2025, 5, 19, 13, 26, 14, 578, DateTimeKind.Utc).AddTicks(8241), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "4b20b985-cba7-46d5-b81e-f6992fa76847", 0, false, "staff@bookstore" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b92c49d3-2503-4c04-abdf-205040d3ca23"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("be2ebb32-a092-4c1e-a764-3adffd28d83f"));

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickupNotes",
                table: "Orders",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "367f5369-3b08-46df-b97e-0fe488b1445c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "fc616588-f462-49c4-ab81-4390ade5cda5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "6500c1b1-9f56-4d6d-b26c-c3b0958d8f1d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiry", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("cbbf4ada-0e20-42cb-92ff-fbedcb019c7a"), 0, "Kathmandu, Nepal", "d22c6915-ccb7-4f53-a4ba-12c644b1aa4a", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAEDwg9vEy4lYXgcsDMv0m8UVe+xUMK0yZEsJplRd9ntKODrbR7yhg5VkFgBcKbsDFQw==", "0987654321", true, null, null, new DateTime(2025, 5, 13, 12, 44, 55, 856, DateTimeKind.Utc).AddTicks(9320), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "601b42cd-7071-47a5-9642-579d7709eae5", 0, false, "staff@bookstore" },
                    { new Guid("f0063b53-5452-4f50-9628-0b0fef5013c4"), 0, "Kathmandu, Nepal", "cb563fcb-ae2e-47e1-b9d7-5015219fb7b2", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAEKv1Bcdv/s4sYnQQMmC8ielrh5Yp3riAq2TI8IVtSBafFpFBSBS2DTrl+bNDT0u+Aw==", "1234567890", true, null, null, new DateTime(2025, 5, 13, 12, 44, 55, 810, DateTimeKind.Utc).AddTicks(5641), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "e306eff3-e119-4dc0-8db6-137e7d1321c0", 0, false, "admin@bookstore" }
                });
        }
    }
}
