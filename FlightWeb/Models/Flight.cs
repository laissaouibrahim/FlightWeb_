using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlightWeb.Models
{
    public class Flight
    {
        [Key] 
        public int FlightId { get; set; }
        [Required,MaxLength(70)]
        [Display(Name ="Flight Name")]
        public string FlightName { get; set; }

        [Required, MaxLength(70)]
        [Display(Name = "Departure Airport")]
        public string Departure { get; set; }
        [Required, MaxLength(70)]
        [Display(Name = "Destination Airport")]
        public string Destination { get; set; }

        [Display(Name = "Departure Airport")]
        public string DepartureCode { get; set; }
        [Display(Name = "Destination Airport")]
        public string DestinationCode { get; set; }

        [Display(Name = "Flight distance (m)")]
        public string FlightDistance { get; set; }

        [Display(Name = "Flight time (s)")]
        public string FlightTime { get; set; }
    }
}