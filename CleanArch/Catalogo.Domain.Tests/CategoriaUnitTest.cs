namespace Catalogo.Domain.Tests;

public class CategoriaUnitTest
{
    [Fact(DisplayName = "Criar categoria válida")]
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
             .WithMessage("valor de Id inválido.");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Categoria("Ca", "url de teste");
        action.Should()
            .Throw<DomainExceptionValidation>()
               .WithMessage("O nome deve ter no mínimo 3 caracteres");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Categoria(string.Empty,"url de teste" );
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome inválido. O nome é obrigatório");
    }

    [Fact]
    public void CreateCategory_MissingImagemUrlValue_DomainExceptionRequiredImagemUr()
    {
        Action action = () => new Categoria("Note de teste", string.Empty);
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome da imagem inválido. O nome é obrigatório");
    }
}