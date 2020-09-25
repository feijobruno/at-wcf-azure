using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class PeopleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Countries_CountryId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_States_StateId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CountryId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_StateId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "People",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "People",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_People_CountryId",
                table: "People",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_StateId",
                table: "People",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Countries_CountryId",
                table: "People",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_States_StateId",
                table: "People",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
