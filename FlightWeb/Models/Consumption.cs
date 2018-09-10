using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlightWeb.Models
{
    public class Consumption
    {
        [Key]
        public int ConsumptionId { get; set; }

        public int FlightId { get; set; }

        [ForeignKey(nameof(FlightId))] 
        public  virtual Flight Flight { get; set; }
       
        [ValidateConsumPer]
        [Display(Name = "Consumption per distance")]
        public decimal? ConsumPerDistance { get; set; }
        [Display(Name = "Consumption per time")]
        public decimal? ConsumPerTime { get; set; }
        [Required]
        [Display(Name = "TakeOff Effort")]
        public decimal TakeoffEffort { get; set; }
        [Display(Name = "Fuel Needed")]
        public decimal Fuel { get; set; }
    }


    //Add annotation to have at least one consumption( per time/distance)
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidateConsumPer : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal? ConsumPerDistance = (decimal?)validationContext.ObjectType.GetProperty("ConsumPerDistance").GetValue(validationContext.ObjectInstance, null);

            decimal? ConsumPerTime = (decimal?)validationContext.ObjectType.GetProperty("ConsumPerTime").GetValue(validationContext.ObjectInstance, null);

            //check at least one has a value
            if (ConsumPerDistance == null && ConsumPerTime == null)
                return new ValidationResult("At least one consumption is required!!");

            return ValidationResult.Success;
        }
    }
}