using System;

namespace Migrations.Models
{
    public abstract class Flights
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureCountry { get; set; }
        public string ArrivalCountry { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public double Price { get; set; }
        public bool Adult { get; set; } = true;
    }
}