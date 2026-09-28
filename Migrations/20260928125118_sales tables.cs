using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrottoPlinius.Migrations
{
    /// <inheritdoc />
    public partial class salestables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItem_Dishes_DishId",
                table: "SalesItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItem_SalesDay_SalesDayId",
                table: "SalesItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesItem",
                table: "SalesItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesDay",
                table: "SalesDay");

            migrationBuilder.RenameTable(
                name: "SalesItem",
                newName: "SalesItems");

            migrationBuilder.RenameTable(
                name: "SalesDay",
                newName: "SalesDays");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItem_SalesDayId_DishId",
                table: "SalesItems",
                newName: "IX_SalesItems_SalesDayId_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItem_DishId",
                table: "SalesItems",
                newName: "IX_SalesItems_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDay_Date",
                table: "SalesDays",
                newName: "IX_SalesDays_Date");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "SalesItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesItems",
                table: "SalesItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesDays",
                table: "SalesDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_Dishes_DishId",
                table: "SalesItems",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItems_SalesDays_SalesDayId",
                table: "SalesItems",
                column: "SalesDayId",
                principalTable: "SalesDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_Dishes_DishId",
                table: "SalesItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesItems_SalesDays_SalesDayId",
                table: "SalesItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesItems",
                table: "SalesItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesDays",
                table: "SalesDays");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SalesItems");

            migrationBuilder.RenameTable(
                name: "SalesItems",
                newName: "SalesItem");

            migrationBuilder.RenameTable(
                name: "SalesDays",
                newName: "SalesDay");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItems_SalesDayId_DishId",
                table: "SalesItem",
                newName: "IX_SalesItem_SalesDayId_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesItems_DishId",
                table: "SalesItem",
                newName: "IX_SalesItem_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDays_Date",
                table: "SalesDay",
                newName: "IX_SalesDay_Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesItem",
                table: "SalesItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesDay",
                table: "SalesDay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItem_Dishes_DishId",
                table: "SalesItem",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesItem_SalesDay_SalesDayId",
                table: "SalesItem",
                column: "SalesDayId",
                principalTable: "SalesDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
