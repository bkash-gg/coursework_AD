using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c28280f3-402d-4569-9b6e-3c976ee50345"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0a030a0-48bb-413d-afc7-dc9e6cc335c2"));

            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Books");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("18341132-8b2f-4637-b76e-e60e4093472c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("95e3fe7c-ecb2-41ea-8cfe-86740595c513"));

            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "c8008c26-aff5-4185-9bf1-735a3ca86fc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "3edd493b-18f1-43bb-af58-e73de92f2946");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "ef3f8dcf-c8c4-4e8d-a6aa-2bf96ae0817e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsEligibleForLoyaltyDiscount", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiry", "RegistrationDate", "RoleId", "SecurityStamp", "TotalOrdersCompleted", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c28280f3-402d-4569-9b6e-3c976ee50345"), 0, "Kathmandu, Nepal", "ad331344-b535-4ab8-a38b-cd17e526cbd0", "staff@bookstore.com", true, "Bookstore Staff", false, true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAENrBNImGG8VAORjMYnWqYPurWQT2qVr412RiMKSAYHPk4Ss9x5dKTW2lgMctHt5+uQ==", "0987654321", true, null, null, new DateTime(2025, 5, 11, 16, 11, 13, 66, DateTimeKind.Utc).AddTicks(6687), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "1aaafafe-9d64-494d-9866-74a9a938aa20", 0, false, "staff@bookstore" },
                    { new Guid("f0a030a0-48bb-413d-afc7-dc9e6cc335c2"), 0, "Kathmandu, Nepal", "ef0b95a2-77ed-4d77-8b6a-7ba79127ae11", "admin@bookstore.com", true, "System Administrator", false, true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAEEZDi/BAAnQ+VPSSVM4uRBCZrjGA5RmpMKBElkP3vsRchO0KaPtL5GoSja79cZ8zww==", "1234567890", true, null, null, new DateTime(2025, 5, 11, 16, 11, 13, 24, DateTimeKind.Utc).AddTicks(5085), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "bc860e91-1b63-455f-af0d-0f4ac126027a", 0, false, "admin@bookstore" }
                });
        }
    }
}
