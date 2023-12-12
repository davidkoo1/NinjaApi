using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaWikiAPI.Migrations
{
    /// <inheritdoc />
    public partial class nullNinjaRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Clans_ClanId",
                table: "Ninjas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Ranks_RankId",
                table: "Ninjas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Villages_VillageId",
                table: "Ninjas");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Ninjas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RankId",
                table: "Ninjas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClanId",
                table: "Ninjas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Clans_ClanId",
                table: "Ninjas",
                column: "ClanId",
                principalTable: "Clans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Ranks_RankId",
                table: "Ninjas",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Villages_VillageId",
                table: "Ninjas",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Clans_ClanId",
                table: "Ninjas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Ranks_RankId",
                table: "Ninjas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ninjas_Villages_VillageId",
                table: "Ninjas");

            migrationBuilder.AlterColumn<int>(
                name: "VillageId",
                table: "Ninjas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RankId",
                table: "Ninjas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClanId",
                table: "Ninjas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Clans_ClanId",
                table: "Ninjas",
                column: "ClanId",
                principalTable: "Clans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Ranks_RankId",
                table: "Ninjas",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ninjas_Villages_VillageId",
                table: "Ninjas",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
