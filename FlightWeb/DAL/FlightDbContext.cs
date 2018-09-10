using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FlightWeb.DAL
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext()
            : base("name=FlightConnString")
        {
           
        }

        public DbSet<Models.Flight> Flights { get; set; }
        public DbSet<Models.Consumption> Consumptions { get; set; }
    }
}