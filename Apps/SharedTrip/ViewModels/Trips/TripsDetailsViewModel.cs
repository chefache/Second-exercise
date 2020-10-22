using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class TripsDetailsViewModel: TripViewModel
    {
        public string ImagePath { get; set; }

        public string StartingPoint { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
