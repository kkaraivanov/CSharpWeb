namespace SharedTrip.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using ExamHelperLibrary.Data;
    using ExamHelperLibrary.Template;
    using FrameworkServer.Controllers;
    using FrameworkServer.Http;
    using Models;
    using ViewModels.TripModels;

    public class TripsController : BaseControllerTemplate
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context) =>
            _context = context;

        [Authorize]
        public HttpResponse All()
        {
            var trips = _context
                .Trips
                .Select(x => new AllTripsViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToLocalTime().ToString("F"),
                    Seats = x.Seats
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() =>
            View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ImagePath))
            {
                var body = Request.Body;
                var parse = HttpUtility.ParseQueryString(body);
                var obj = new Trip
                {
                    ImagePath = HttpUtility.UrlDecode(parse["ImagePath"])
                };

                viewModel.ImagePath = obj.ImagePath;
            }

            if (!ModelIsVlaid(viewModel))
            {
                return Error(Errors.ToList());
            }

            var trip = new Trip
            {
                StartPoint = viewModel.StartPoint,
                EndPoint = viewModel.EndPoint,
                DepartureTime = DateTime.Parse(viewModel.DepartureTime),
                Seats = viewModel.Seats,
                Description = viewModel.Description,
                ImagePath = viewModel.ImagePath
            };

            _context.Trips.Add(trip);
            _context.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = _context
                .Trips
                .Where(x => x.Id == tripId)
                .Select(x => new DetailsTripViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToLocalTime().ToString("F"),
                    Seats = x.Seats,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    IsUsed = x.UserTrips.Any(x => x.TripId == tripId && x.UserId == User.Id)
                })
                .FirstOrDefault();
            
            if (trip.Seats == 0)
            {
                trip.IsUsed = true;
            }

            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var trip = _context
                .Trips
                .FirstOrDefault(x => x.Id == tripId);
            var userOnTripExist = trip.UserTrips.Any(x => x.TripId == trip.Id && x.UserId == User.Id);

            if (userOnTripExist)
            {
                return Details(tripId);
            }

            var userToTrip = new UserTrip
            {
                UserId = User.Id,
                TripId = trip.Id
            };

            if (trip.Seats > 0)
            {
                trip.Seats--;
            }

            _context.UserTrips.Add(userToTrip);
            _context.Trips.Update(trip);
            _context.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}