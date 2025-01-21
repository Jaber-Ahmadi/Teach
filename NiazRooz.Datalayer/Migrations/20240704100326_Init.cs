using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NiazRooz.Datalayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTBL",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(maxLength: 200, nullable: false),
                    ParentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTBL", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_PermissionTBL_PermissionTBL_ParentID",
                        column: x => x.ParentID,
                        principalTable: "PermissionTBL",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleTBL",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTBL", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserTBL",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserAvatar = table.Column<string>(maxLength: 200, nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTBL", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionTBL",
                columns: table => new
                {
                    RP_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionTBL", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RolePermissionTBL_PermissionTBL_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionTBL",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionTBL_RoleTBL_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTBL",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleTBL",
                columns: table => new
                {
                    UR_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleTBL", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_UserRoleTBL_RoleTBL_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleTBL",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleTBL_UserTBL_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTBL",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTBL_ParentID",
                table: "PermissionTBL",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionTBL_PermissionId",
                table: "RolePermissionTBL",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionTBL_RoleId",
                table: "RolePermissionTBL",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTBL_RoleId",
                table: "UserRoleTBL",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTBL_UserId",
                table: "UserRoleTBL",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionTBL");

            migrationBuilder.DropTable(
                name: "UserRoleTBL");

            migrationBuilder.DropTable(
                name: "PermissionTBL");

            migrationBuilder.DropTable(
                name: "RoleTBL");

            migrationBuilder.DropTable(
                name: "UserTBL");
        }
    }
}
