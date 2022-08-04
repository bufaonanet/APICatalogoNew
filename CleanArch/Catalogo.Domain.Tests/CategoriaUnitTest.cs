namespace Catalogo.Domain.Tests;

public class CategoriaUnitTest
{
    [Fact(DisplayName = "Criar categoria v�lida")]
    public void CreateCategory_WithValidParameters_ShouldReturnValidObject()
    {
        Action action = () =>
            new Categoria("Nova Categoria", "Imagem url");

        action.Should()
            .NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Categoria(-1, "Category Name ", "Imagem url");

        action.Should()
            .Throw<DomainExceptionValidation>()
             .WithMessage("valor de Id inv�lido.");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Categoria("Ca", "url de teste");
        action.Should()
            .Throw<DomainExceptionValidation>()
               .WithMessage("O nome deve ter no m�nimo 3 caracteres");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Categoria(string.Empty,"url de teste" );
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome inv�lido. O nome � obrigat�rio");
    }

    [Fact]
    public void CreateCategory_MissingImagemUrlValue_DomainExceptionRequiredImagemUr()
    {
        Action action = () => new Categoria("Note de teste", string.Empty);
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome da imagem inv�lido. O nome � obrigat�rio");
    }
}