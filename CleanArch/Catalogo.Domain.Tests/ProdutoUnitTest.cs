namespace Catalogo.Domain.Tests;

public class ProdutoUnitTest
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () =>
            new Produto(1, "Product Name", "Product Description", 9.99m, "imagemUrl", 99, DateTime.Now);

        action.Should()
            .NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => 
            new Produto(-1, "Product Name", "Product Description", 9.99m, "imagemUrl", 99, DateTime.Now);

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Id inválido.");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => 
            new Produto(1, "Pr", "Product Description", 9.99m, "imagemUrl", 99, DateTime.Now);

        action.Should().Throw<DomainExceptionValidation>()
             .WithMessage("O nome deve ter no mínimo 3 caracteres");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {       
        Action action = () =>
            new Produto(1, "Product Name", "Product Description", 9.99m, string.Empty, 99, DateTime.Now);

        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () =>
           new Produto(1, "Product Name", "Product Description", 9.99m, string.Empty, value, DateTime.Now);

        action.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Estoque inválido");
    }
}
