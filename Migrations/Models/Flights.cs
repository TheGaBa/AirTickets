using System;

namespace Migrations.Models
{
    public abstract class Flights
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime arrivalDate { get; set; }
        public string departureCountry { get; set; }
        public string arrivalCountry { get; set; }
        public string departureCity { get; set; }
        public string arrivalCity { get; set; }
        public double price { get; set; }
        public bool adult { get; set; } = true;
    }
}