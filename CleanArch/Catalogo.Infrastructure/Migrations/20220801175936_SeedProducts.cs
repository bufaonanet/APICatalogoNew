using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Infrastructure.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
                                "Values('Caderno','Caderno Escolar',5.45,'caderno.jpg',50,now(),(Select Id from Categorias where Nome='Material Escolar'))");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
                                "Values('Samsung','Celualr Samsung',980.50,'celular.jpg',10,now(),(Select Id from Categorias where Nome='Eletrônicos'))");

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
                                "Values('Mochila','Mochilas Escolar',6.75,'mochila.jpg',20,now(),(Select Id from Categorias where Nome='Acessórios'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
