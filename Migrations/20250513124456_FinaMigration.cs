using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class FinaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("18341132-8b2f-4637-b76e-e60e4093472c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95e3fe7c-ecb2-41ea-8cfe-86740595c513"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cbbf4ada-0e20-42cb-92ff-fbedcb019c7a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0063b53-5452-4f50-9628-0b0fef5013c4"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "07665b77-7e85-4086-9c57-b2a39e9c656c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "4d592b01-9f3e-4844-9500-e2f9bfc425f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "2e073091-dadc-408e-80b8-99247a3058e3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiry", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("18341132-8b2f-4637-b76e-e60e4093472c"), 0, "Kathmandu, Nepal", "2145d63f-65e6-4c6c-9b5f-d654729e6ab8", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAEGh+S1b/nFWffFIxSoJ18HWFHXccaFvb/n1y3tyb7Mi6NgbvgKrpLlbMT+RfHyCmLA==", "0987654321", true, null, null, new DateTime(2025, 5, 12, 20, 18, 6, 103, DateTimeKind.Utc).AddTicks(9113), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "f4dbfe27-db31-4309-8467-a6ef6d7c0d9b", 0, false, "staff@bookstore" },
                    { new Guid("95e3fe7c-ecb2-41ea-8cfe-86740595c513"), 0, "Kathmandu, Nepal", "d269b20f-bbd8-4848-8b5e-ba6c8bba0b1d", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAECnUtxonwIYje4VSNeJSTE/andSkmzs9zV3a0sl0lxEydoCAzkciNBKG9mbzc+E8fg==", "1234567890", true, null, null, new DateTime(2025, 5, 12, 20, 18, 6, 63, DateTimeKind.Utc).AddTicks(4171), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "971d5cb8-eff5-4b99-9097-054838d855ad", 0, false, "admin@bookstore" }
                });
        }
    }
}
