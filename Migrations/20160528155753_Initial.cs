using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamTodo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    IsComplete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TodoTypeId = table.Column<int>(nullable: false),
                    TodoTypeId1 = table.Column<int>(nullable: true),
                    TodoUserId = table.Column<int>(nullable: false),
                    TodoUserId1 = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_TodoTypes_TodoTypeId1",
                        column: x => x.TodoTypeId1,
                        principalTable: "TodoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Todos_TodoUsers_TodoUserId1",
                        column: x => x.TodoUserId1,
                        principalTable: "TodoUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoTypeId1",
                table: "Todos",
                column: "TodoTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoUserId1",
                table: "Todos",
                column: "TodoUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "TodoTypes");

            migrationBuilder.DropTable(
                name: "TodoUsers");
        }
    }
}
