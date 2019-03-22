using FluentValidation;
using FluentValidation.Results;

namespace SmartWeChat.DTO
{
    public abstract class IRequestModel
    {
        private ValidationResult _validationResult;

        public abstract bool Validate();

        protected bool Validate<T>(T validator) where T : IValidator
        {
            _validationResult = validator.Validate(this);
            return _validationResult.IsValid;
        }

        public string ErrorMessages => _validationResult == null ? string.Empty : _validationResult.ToString("|");
    }
}
