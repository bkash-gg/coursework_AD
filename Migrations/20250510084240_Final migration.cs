using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class Finalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69daaad5-eac4-4c6f-ab7c-cafce1b7889c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c4ca2613-69c6-4689-989a-c8d93037c93e"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "270556a9-e4e7-40cd-9982-519fce494d44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "9eff6fbf-2f7b-4b5a-a215-ee9869359cd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "8633cddc-f350-417b-9b00-f89822f64ac9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("a3c987c0-7ec8-4182-b08b-750014d68e0d"), 0, "Kathmandu, Nepal", "2eb831ee-203d-44a3-a9bb-26d4778622fe", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAECxwaKvH5z9oKgqf5DWbtPRNFFJOjSxLCiv6EGaXFUBb2k74dmpxGn1u8zXznqQKQg==", "0987654321", true, new DateTime(2025, 5, 10, 8, 42, 40, 35, DateTimeKind.Utc).AddTicks(4664), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "f396f772-fd03-4691-a186-b720b9b31667", 0, false, "staff@bookstore" },
                    { new Guid("f6c8f431-5953-4544-a124-2b1bb3b439c9"), 0, "Kathmandu, Nepal", "6277483f-930d-4689-95f3-6aa92f5ac858", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAEI1dVNN1NyMqdoTqY3J+nVmJY4RYcIVvxq1SC6boXeJhjX3d2eP/UZSRZKC7ccE16Q==", "1234567890", true, new DateTime(2025, 5, 10, 8, 42, 39, 988, DateTimeKind.Utc).AddTicks(7742), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "a8f244dd-7869-46b4-a33e-60304807d4fa", 0, false, "admin@bookstore" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a3c987c0-7ec8-4182-b08b-750014d68e0d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f6c8f431-5953-4544-a124-2b1bb3b439c9"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "2e9d1ca2-83fd-44cd-80b5-5f2ca8f8a573");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "1f9f8489-4b06-464a-8122-3dbd955e7f82");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "3f39bee8-26a3-409f-ba1e-1d3a3f90f8fa");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("69daaad5-eac4-4c6f-ab7c-cafce1b7889c"), 0, "Kathmandu, Nepal", "42e44fe7-797a-416b-893d-66761c9e9c30", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAEFEnhLYQMBe58bhKEo5uG8c2AVzcFKJoTH/9MADUKHXQJ2pnX5ftxJmfJtAfUYaNtg==", "0987654321", true, new DateTime(2025, 5, 7, 15, 14, 48, 760, DateTimeKind.Utc).AddTicks(8317), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "1ff9fefa-a235-4075-b400-c4aed916717c", 0, false, "staff@bookstore" },
                    { new Guid("c4ca2613-69c6-4689-989a-c8d93037c93e"), 0, "Kathmandu, Nepal", "ee81d9ce-806d-40f1-95ba-641b32ccbffb", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAELJrJRaeIdQtdGz9BEEOwU3y1U0CkNpg8L91NGHI3+i14iY8XamphKLlI69Ai56H0g==", "1234567890", true, new DateTime(2025, 5, 7, 15, 14, 48, 720, DateTimeKind.Utc).AddTicks(1728), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "6f8117a6-8a9d-4c99-ad7c-007cfc6d045b", 0, false, "admin@bookstore" }
                });
        }
    }
}
