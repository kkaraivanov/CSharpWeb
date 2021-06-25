namespace ExamHelperLibrary.Services
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IValidator
    {
        bool ModelIsValid(object obj);

        IReadOnlyCollection<string> Errors { get; }

        void AddError(string error);
    }

    public class Validator : IValidator
    {
        private readonly ObjectAttributeValidator _validator;
        private ValidatorResult _modelValidate;

        public bool ModelIsValid(object obj) =>
            GetModelValidateResult(obj).IsValid;

        public IReadOnlyCollection<string> Errors =>
            _modelValidate.Errors.ToList();

        public void AddError(string error) =>
            _modelValidate.Errors.Add(error);

        public Validator()
        {
            _validator = new ObjectAttributeValidator();
        }

        private ValidatorResult GetModelValidateResult(object obj) =>
            _modelValidate = _validator.Validate(obj);
    }
}