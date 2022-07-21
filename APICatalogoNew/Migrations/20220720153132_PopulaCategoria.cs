using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogoNew.Migrations
{
    public partial class PopulaCategoria : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Bebidas','bebidas.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Lanches','lanches.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Sobremesas','sobremesa.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
