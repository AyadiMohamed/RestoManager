using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoManager_AyadiMed.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "resto");

            migrationBuilder.CreateTable(
                name: "TProprietair",
                schema: "resto",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GsmProp = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProprietair", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "TRestaurant",
                schema: "resto",
                columns: table => new
                {
                    CodeResto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Tunisienne"),
                    VilleResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TelResto = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    NumProp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRestaurant", x => x.CodeResto);
                    table.ForeignKey(
                        name: "Relation_Propio_Resto",
                        column: x => x.NumProp,
                        principalSchema: "resto",
                        principalTable: "TProprietair",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRestaurant_NumProp",
                schema: "resto",
                table: "TRestaurant",
                column: "NumProp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRestaurant",
                schema: "resto");

            migrationBuilder.DropTable(
                name: "TProprietair",
                schema: "resto");
        }
    }
}
