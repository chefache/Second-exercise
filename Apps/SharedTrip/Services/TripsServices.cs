using SharedTrip.Data;
using System;
using System.Collections.Generic;
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
    }
}
