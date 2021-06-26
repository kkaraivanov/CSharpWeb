namespace ExamHelperLibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using SharedTrip.Models;
    using Template;

    public class User : BaseUserTemplate<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            UserTrips = new HashSet<UserTrip>();
        }

        // TODO: Add other user properties
        public virtual ICollection<UserTrip> UserTrips { get; set; }
    }
}