using SharedTrip.Data;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsServices services;

        public TripsController(ITripsServices services)
        {
            this.services = services;
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Start point requaered.");
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("End poin requaered.");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats must be between 2 and 6.");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.Error("Description must be less then 80 characters.");
            }

            this.services.Create(input.StartPoint, input.EndPoint, input.DepartureTime, input.ImagePath, input.Description, input.Seats);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            var trips = this.services.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            var trip = this.services.GetDetails(tripId);
            return this.View(trip);
        }


        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.services.HasAvailableSeats(tripId))
            {
                return this.Error("No seats available");
            }

            var userId = this.GetUserId();
            this.services.AddUserToTrip(userId, tripId);

            return this.Redirect("/Trips/All");
        }
    }
}
