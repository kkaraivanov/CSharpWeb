namespace SharedTrip.ViewModels.TripModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddTripViewModel
    {
        //startPoint: Banya
        //endPoint: Sofia
        //departureTime: 30.08.2021 
        //imagePath: https://images.unsplash.com/photo-1597404294360-feeeda04612e?ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8Y2FyfGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&w=1000&q=80
        //seats: 4
        //description: slkdfjlskdfjlsdkfj
        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        [Required]
        [Range(2, 6)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}