using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PythonLearn.DAL.Migrations
{
    /// <inheritdoc />
    public partial class userc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "BirthDay", "Email", "Login", "Name", "Password", "RoleId", "SecondName", "avatar" },
                values: new object[] { 1, "text", new DateTime(1995, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "3kers@mail.ry", "kostaqw", "Kosta", "E10ADC3949BA59ABBE56E057F20F883E", 1, "Chistiakov", new byte[] { 1, 2, 3, 4, 5 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
