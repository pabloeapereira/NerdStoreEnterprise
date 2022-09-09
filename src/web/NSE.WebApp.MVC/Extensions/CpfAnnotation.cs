using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using NSE.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Extensions
{
    public class CpfAnnotation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.Validar(value.ToString()) ? ValidationResult.Success :
                new ValidationResult("CPF em formato inválido");
        }
    }

    public class CpfAttributeAdapter : AttributeAdapterBase<CpfAnnotation>
    {

        public CpfAttributeAdapter(CpfAnnotation attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {

        }
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-cpf", GetErrorMessage(context));
        }
        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            "CPF em formato inválido";
    }

    public class CpfValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is CpfAnnotation CpfAttribute)
            {
                return new CpfAttributeAdapter(CpfAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}