namespace ExamHelperLibrary.Services
{
    using System.Collections.Generic;

    public interface IValidator
    {
        bool ModelIsValid(object obj);

        IReadOnlyCollection<string> Errors { get; }

        void AddError(string error);
    }
}