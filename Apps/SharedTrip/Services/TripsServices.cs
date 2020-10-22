using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
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
                   StartingPoint = x.StartPoint,
                   EndPoint = x.EndPoint,
                   Seats = x.Seats,
                   Description = x.Description,
                   ImagePath = x.ImagePath,
                   Id = x.Id,
                })
                .FirstOrDefault();
        }
    }
}
