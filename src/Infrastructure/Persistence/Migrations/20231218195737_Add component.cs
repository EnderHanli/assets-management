using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addcomponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_Department_DepartmentId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Department_DepartmentId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Department_DepartmentId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Department_DepartmentId",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Department_DepartmentId",
                table: "Licenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_Departments_DepartmentId",
                table: "Accessories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                table: "Assets",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Departments_DepartmentId",
                table: "Components",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Departments_DepartmentId",
                table: "Consumables",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Departments_DepartmentId",
                table: "Licenses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_Departments_DepartmentId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Departments_DepartmentId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_Departments_DepartmentId",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Departments_DepartmentId",
                table: "Licenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_Department_DepartmentId",
                table: "Accessories",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Department_DepartmentId",
                table: "Assets",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Department_DepartmentId",
                table: "Components",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_Department_DepartmentId",
                table: "Consumables",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Department_DepartmentId",
                table: "Licenses",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
