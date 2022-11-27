using FluentValidation.Results;
using NSE.Core.Data;

namespace NSE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message) =>
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));

        protected void AddErrors(ValidationResult validationResult) =>
            validationResult?.Errors.ForEach(e => AddError(e.ErrorMessage));

        protected async ValueTask<ValidationResult> CommitAsync(IUnitOfWork uow)
        {
            if (!await uow.CommitAsync()) AddError("Houve um erro ao persistir os dados.");

            return ValidationResult;
        }
    }
}