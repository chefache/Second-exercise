using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsServices : ITripsServices
    {
        private readonly ApplicationDbContext db;

        public TripsServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool HasAvailableSeats(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                 .Select(x => new 
                 {
                     x.Seats, TakenSeats = x.UserTrips.Count() 
                 })
                 .FirstOrDefault();

            var availableSeats = trip.Seats - trip.TakenSeats;
            if (availableSeats <= 0 )
            {
                return false;
            }

            return true;
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var userInTrip = this.db.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);

            if (userInTrip)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId,
            };

            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();
            return true;
        }

        public void Create(string startPoint, string endPoint, DateTime departureTime, string imagePath, string description, int seats)
        {
            var trip = new Trip
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = departureTime,
                ImagePath = imagePath,
                Description = description,
                Seats = seats,
            };

            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = this.db.Trips.Select(x => new TripViewModel
            {
                DepartureTime = x.DepartureTime,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                Id = x.Id,
                AvailableSeats = x.Seats - x.UserTrips.Count(),              
            }).ToList();

            return trips;
        }

        public TripsDetailsViewModel GetDetails(string id)
        {
            return this.db.Trips.Where(x => x.Id == id)
                .Select(x => new TripsDetailsViewModel
                {
                   DepartureTime = x.DepartureTime,
                   StartPoint = x.StartPoint,
                   EndPoint = x.EndPoint,
                   AvailableSeats = x.Seats,
                   Description = x.Description,
                   ImagePath = x.ImagePath,
                   Id = x.Id,
                })
                .FirstOrDefault();
        }
    }
}
