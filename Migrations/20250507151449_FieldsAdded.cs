using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class FieldsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("880f2107-27af-44f5-80d7-489e2d980e26"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8cbcbd0-f41a-4c6f-9fb8-aea7094c8f60"));

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimCode",
                table: "Orders",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

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

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.AddColumn<bool>(
                name: "UsedBulkDiscount",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UsedLoyaltyDiscount",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemCount",
                table: "Carts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Carts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "CartItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Authors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEligibleForLoyaltyDiscount",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalOrdersCompleted",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Announcements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Announcements",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Announcements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Announcements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Announcements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Announcements",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69daaad5-eac4-4c6f-ab7c-cafce1b7889c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c4ca2613-69c6-4689-989a-c8d93037c93e"));

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
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

            migrationBuilder.DropColumn(
                name: "UsedBulkDiscount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UsedLoyaltyDiscount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ItemCount",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsEligibleForLoyaltyDiscount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalOrdersCompleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Publishers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimCode",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Authors",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Announcements",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Announcements",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Announcements",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Announcements",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                column: "ConcurrencyStamp",
                value: "2829a150-9dd1-4617-9e4c-76f2341a5248");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                column: "ConcurrencyStamp",
                value: "bd65241f-f29c-4690-9a0a-2e556a9ceeb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                column: "ConcurrencyStamp",
                value: "9135c6d1-5df1-4650-868e-c614b0d727f4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("880f2107-27af-44f5-80d7-489e2d980e26"), 0, "Kathmandu, Nepal", "50da9f29-9ea4-4181-a16e-16d8b6778b6b", "staff@bookstore.com", true, "Bookstore Staff", true, null, "STAFF@BOOKSTORE.COM", "STAFF@BOOKSTORE", "AQAAAAIAAYagAAAAEJbf+0fMbuImsYGUJ/k1N9e+fuuwcL1YPSNXOE68lceHBCYoDdGylDL9u58kP8WD0A==", "0987654321", true, new DateTime(2025, 5, 5, 21, 4, 6, 95, DateTimeKind.Utc).AddTicks(9238), new Guid("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"), "fdfe0e04-0767-417e-a197-0680918a093c", false, "staff@bookstore" },
                    { new Guid("f8cbcbd0-f41a-4c6f-9fb8-aea7094c8f60"), 0, "Kathmandu, Nepal", "a301cafa-d815-4d1f-a3fc-e10c6dab0250", "admin@bookstore.com", true, "System Administrator", true, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE", "AQAAAAIAAYagAAAAEIWYjmXdNbKzqx3NpbKk0ptlj9GuSCdXb1ez4LYQWwbUtsZcf4LJT80LRZ5cUR3KOw==", "1234567890", true, new DateTime(2025, 5, 5, 21, 4, 6, 54, DateTimeKind.Utc).AddTicks(7593), new Guid("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"), "eaf31b0b-96f9-4c5e-bdc8-2b67613b2901", false, "admin@bookstore" }
                });
        }
    }
}
