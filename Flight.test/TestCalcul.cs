using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FlightWeb.Controllers;
using FlightWeb.DAL;
using FlightWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Flight.test
{
    [TestClass]
    public class TestCalcul
    { 
        [TestMethod]
        public void TestCalculation()
        {

            //Arrange
            var controller = new ConsumptionController();
            var cons = new Consumption
            {
                ConsumPerDistance = 10,
                ConsumPerTime = 8,
                TakeoffEffort = 10
            }; 
            //Act
            var result = controller.Calculate(cons,"1000","1000",1) ; 
            //Assert
            Assert.AreEqual(result, 8010);

        }

        [TestMethod]
        public void ThenReturnTheFlightViewModel()
        {
            // Arrange
            var flights = new List<FlightWeb.Models.Flight>() 
            {
                new FlightWeb.Models.Flight { FlightId = 1, Departure = "Orly", Destination = "Paris" },
                new FlightWeb.Models.Flight { FlightId = 2, Departure = "Paris", Destination = "Rouen" },
                new FlightWeb.Models.Flight { FlightId = 3, Departure = "Nice", Destination = "Lyon" } 
            };
            var flightRepository = new Mock<ICustomRepository<FlightWeb.Models.Flight>>();

                flightRepository.Setup(m => m.GetAllData()).Returns(flights.AsQueryable());
            var controller = new FlightController(flightRepository.Object);
                    // Act 
                    var result = controller.Index(null,null,null) as ViewResult;
                    var model = result.Model as IEnumerable<FlightWeb.Models.Flight>;
                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(3, model.Cast<Object>().Count());
                }

        [TestMethod]
        public void ThenReturnTheConsumptionViewModel()
        {
            // Arrange
            var cons = new List<Consumption>()
            {
                new Consumption { ConsumptionId = 1, ConsumPerDistance = 10, ConsumPerTime = 5, TakeoffEffort=1},
                new Consumption { ConsumptionId = 2, ConsumPerDistance = 20, ConsumPerTime = 3, TakeoffEffort=2 },
                new Consumption { ConsumptionId = 3, ConsumPerDistance = 20, ConsumPerTime = 6, TakeoffEffort=3}
            };
            var consumptionRepository = new Mock<ICustomRepository<Consumption>>();
            consumptionRepository.Setup(m => m.SelectDataById2(x => x.Flight)).Returns(cons.AsQueryable());
            var controller = new ConsumptionController(consumptionRepository.Object);
            // Act 
            var result = controller.Index(null, null, null) as ViewResult;
            var model = result.Model as IEnumerable<Consumption>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, model.Cast<Object>().Count());
        }
    }
}
