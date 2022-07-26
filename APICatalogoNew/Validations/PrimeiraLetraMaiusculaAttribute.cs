using System.ComponentModel.DataAnnotations;

namespace APICatalogoNew.Validations;

public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var primemiraLetra = value.ToString()?[0].ToString();
        if(primemiraLetra != primemiraLetra?.ToUpperInvariant())
        {
            return new ValidationResult("A primeira letra do nome do produto de ver maíuscula");
        }

        return ValidationResult.Success;
    }
}