using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using FlightWeb.DAL;
using FlightWeb.Models;

namespace FlightWeb.Controllers
{
    public class ConsumptionController : Controller
    {
        //private FlightDbContext db = new FlightDbContext();


       
        private ICustomRepository<Consumption> empRepository = null;
        private ICustomRepository<Flight> empRepository2 = null; 
        public ConsumptionController()
        {
            this.empRepository = new MyCustomRepository<Consumption>();
            this.empRepository2 = new MyCustomRepository<Flight>();
        }
        public ConsumptionController(ICustomRepository<Consumption> repository)
        {
            empRepository = repository;
        }

        // GET: Consumption
        public ActionResult Index(string thisFilter, string searchString, int? page)
        {
           
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = thisFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var Consumptions = empRepository.SelectDataById2(x => x.Flight);

             

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(Consumptions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Consumption/Details/5
        public ActionResult Details(int? id)
        {

            if (id != null)
            {

                var consumption = empRepository.SelectDataById2(x => x.Flight).Where(ind => ind.ConsumptionId == id).FirstOrDefault(); 
                return View(consumption);
            }
            else
            {
                return RedirectToAction("Index");
            }

 
        }

        // GET: Consumption/Create/IdFLight
        public ActionResult Create(int? id)
        { 
            if(id != null)
            {
            var flight = empRepository2.SelectDataById(id);
            Consumption cons = new Consumption();
            cons.Flight = flight;
            return View(cons);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
           
        }

        // POST: Consumption/Create
     
        [HttpPost] 
        public ActionResult Create( Consumption emp)
        {
            try
            {
                if (ModelState.IsValid)
            {
                    emp.FlightId = emp.Flight.FlightId;
                    emp.Flight = null;
                empRepository.InsertRecord(emp);
                empRepository.SaveRecord();
                return RedirectToAction("Index");
            }
        }
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to save record.");
            }
            return View(emp);
        }

        // GET: Consumption/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var consumption = empRepository.SelectDataById2(x => x.Flight).Where(ind => ind.ConsumptionId == id).FirstOrDefault();
            consumption.FlightId = consumption.Flight.FlightId; 
            return View(consumption); 
        }

        // POST: Consumption/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Consumption emp)
        {
            try
            {


                if (ModelState.IsValid)
                {
                     emp.Flight = null;

                    empRepository.Update(emp);
                    empRepository.SaveRecord();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to edit Consumption record.");
            }

            return View(emp);
        }

        // GET: Consumption/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(empRepository.SelectDataById(id)); 

        }

        // POST: Consumption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empRepository.DeleteRecord(id);
                    empRepository.SaveRecord();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete the record.");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               // db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: Consumption/Create

        [HttpPost]
        public decimal Calculate(Consumption emp, string FlightDistance, string FlightTime, int MethodCalcul)
        {
            if (MethodCalcul == 1)
            {
                return Math.Round((Convert.ToDecimal(FlightTime) * Convert.ToDecimal(emp.ConsumPerTime)) + emp.TakeoffEffort);
            }
            else if(emp.ConsumPerDistance.HasValue)
            { 
                return Math.Round(Convert.ToDecimal(FlightDistance) * Convert.ToDecimal(emp.ConsumPerDistance) + emp.TakeoffEffort); 
            }
            return 0;
        }
    }
}
