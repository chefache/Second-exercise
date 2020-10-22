using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.Data
{
    public class Trip
    {
        public Trip()
        {
            this.UserTrips = new HashSet<UserTrip>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [MaxLength(6)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual IEnumerable<UserTrip> UserTrips { get; set; }
    }
}
