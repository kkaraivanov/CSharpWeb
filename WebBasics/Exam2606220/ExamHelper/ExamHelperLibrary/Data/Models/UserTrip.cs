namespace SharedTrip.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using ExamHelperLibrary.Data.Models;

    public class UserTrip
    {
        //Has UserId – a string
        //Has User – a User object
        //Has TripId– a string
        //Has Trip – a Trip object

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Trip))]
        public string TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
