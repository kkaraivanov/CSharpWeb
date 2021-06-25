namespace ExamHelperLibrary.Data.Models
{
    using System;
    using Template;

    public class User : BaseUserTemplate<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool IsMechanic { get; set; }
    }
}