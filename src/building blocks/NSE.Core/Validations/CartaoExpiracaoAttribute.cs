using System.ComponentModel.DataAnnotations;

namespace NSE.Core.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class CartaoExpiracaoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Expiração do cartão não informado");

            var mes = value.ToString()?.Split('/')[0];
            var ano = $"20{value.ToString()?.Split('/')[1]}";

            if (!int.TryParse(mes, out var month) ||
                !int.TryParse(ano, out var year))
            {
                return new ValidationResult("Expiração do cartão inválida");
            }

            var d = new DateTime(year, month, 1);
            return d > DateTime.UtcNow ? ValidationResult.Success : new ValidationResult("Expiração do cartão inválida");

        }
    }
}