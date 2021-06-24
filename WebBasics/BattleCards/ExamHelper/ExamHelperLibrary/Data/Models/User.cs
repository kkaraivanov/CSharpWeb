namespace ExamHelperLibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Template;

    public class User : BaseUserTemplate<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            UserCards = new HashSet<UserCard>();
        }

        public virtual ICollection<UserCard> UserCards { get; set; }
    }
}