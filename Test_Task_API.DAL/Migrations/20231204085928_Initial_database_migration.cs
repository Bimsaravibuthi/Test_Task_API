using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Task_API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_database_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USR_NAME = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    USR_EMAIL = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    USR_PASSWORD = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    USR_USERNAME = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    USR_TPN = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    USR_ACTIVESTATUS = table.Column<bool>(type: "bit", nullable: false),
                    USR_STATUS = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    USR_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TSK_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TSK_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSK_PRIORITY = table.Column<int>(type: "int", nullable: false),
                    TSK_CREATED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TSK_END = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tbl_Tasks_Tbl_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Tbl_Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Tasks_UserID",
                table: "Tbl_Tasks",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Tasks");

            migrationBuilder.DropTable(
                name: "Tbl_Users");
        }
    }
}
