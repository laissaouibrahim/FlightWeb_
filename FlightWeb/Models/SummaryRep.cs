using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightWeb.Models
{
    public class SummaryRep
    {

        public string FlightName { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; } 
        public string FlightDistance { get; set; }
        public string FlightTime { get; set; }

        public decimal ConsumPerDistance { get; set; }
        public decimal ConsumPerTime { get; set; }
        public decimal TakeoffEffort { get; set; }
        public decimal Fuel { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Consumption, SummaryRep>()
               .ForMember(vm => vm.FlightName, map => map.MapFrom(m => m.Flight.FlightName))
               .ForMember(vm => vm.Departure, map => map.MapFrom(m => m.Flight.Departure))
               .ForMember(vm => vm.Destination, map => map.MapFrom(m => m.Flight.Destination))
               .ForMember(vm => vm.FlightDistance, map => map.MapFrom(m => m.Flight.FlightDistance))
               .ForMember(vm => vm.FlightTime, map => map.MapFrom(m => m.Flight.FlightTime))
               .ForMember(vm => vm.ConsumPerDistance, map => map.MapFrom(m => m.ConsumPerDistance))
               .ForMember(vm => vm.ConsumPerTime, map => map.MapFrom(m => m.ConsumPerTime))
              .ForMember(vm => vm.Fuel, map => map.MapFrom(m => m.Fuel));


        }
    }
}