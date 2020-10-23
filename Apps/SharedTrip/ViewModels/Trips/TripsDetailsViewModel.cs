using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class TripsDetailsViewModel: TripViewModel
    {
        public string ImagePath { get; set; }


        public string Description { get; set; }

        public string DepartureTtimeHtmlFormated => this.DepartureTime.ToString("s");
    }
}
