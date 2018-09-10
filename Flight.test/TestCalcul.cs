using System;
using FlightWeb.Controllers;
using FlightWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
