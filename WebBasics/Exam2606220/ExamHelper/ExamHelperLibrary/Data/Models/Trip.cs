namespace SharedTrip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ExamHelperLibrary.Template;

    public class Trip : BaseDataTemplate<string>
    {
        //Has an Id – a string, Primary Key
        //Has a StartPoint – a string (required)
        //Has a EndPoint – a string (required)
        //Has a DepartureTime – a datetime(required)
        //Has a Seats – an integer with min value 2 and max value 6 (required)
        //Has a Description – a string with max length 80 (required)
        //Has a ImagePath – a string
        //Has UserTrips collection – a UserTrip type
        public Trip()
        {
            Id = Guid.NewGuid().ToString();
            UserTrips = new HashSet<UserTrip>();
        }

        [Required]
        public string StartPoint { get; set; }
        
        [Required]
        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        [Range(2, 6)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; set; }

    }
}
