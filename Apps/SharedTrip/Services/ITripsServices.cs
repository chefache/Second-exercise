﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsServices
    {
        void Create(string startPoint, string endPoint, DateTime departureTime, string imagePath, string description, int seats);

    }
}
