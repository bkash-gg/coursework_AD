using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AD_Coursework.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a3c987c0-7ec8-4182-b08b-750014d68e0d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f6c8f431-5953-4544-a124-2b1bb3b439c9"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c28280f3-402d-4569-9b6e-3c976ee50345"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0a030a0-48bb-413d-afc7-dc9e6cc335c2"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

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
    }
}
