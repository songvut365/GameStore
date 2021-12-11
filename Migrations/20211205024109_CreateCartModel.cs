using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Migrations
{
    public partial class CreateCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Game_gameId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_gameId",
                table: "Cart");

            migrationBuilder.AlterColumn<int>(
                name: "gameId",
                table: "Cart",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "totalPrice",
                table: "Cart",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Cart");

            migrationBuilder.AlterColumn<int>(
                name: "gameId",
                table: "Cart",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_gameId",
                table: "Cart",
                column: "gameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Game_gameId",
                table: "Cart",
                column: "gameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
