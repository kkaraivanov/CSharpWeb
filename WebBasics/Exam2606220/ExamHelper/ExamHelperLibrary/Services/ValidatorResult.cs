namespace ExamHelperLibrary.Services
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidatorResult
    {
        public bool IsValid => 
            !Errors.Any();

        public ICollection<string> Errors { get; }

        public ValidatorResult() =>
            Errors = new List<string>();
    }
}