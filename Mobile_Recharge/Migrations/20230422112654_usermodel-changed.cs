using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Recharge.Migrations
{
    public partial class usermodelchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "userPlanHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "userPlanHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
