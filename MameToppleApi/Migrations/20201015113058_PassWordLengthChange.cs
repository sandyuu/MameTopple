using Microsoft.EntityFrameworkCore.Migrations;

namespace MameToppleApi.Migrations
{
    public partial class PassWordLengthChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    nickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    win = table.Column<int>(type: "int", nullable: true),
                    lose = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.account);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
