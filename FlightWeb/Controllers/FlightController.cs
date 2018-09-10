using FlightWeb.DAL;
using FlightWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Text;
using FlightWeb.Reports;
using AutoMapper;

namespace FlightWeb.Controllers
{
    public class FlightController : Controller
    {
        private ICustomRepository<Flight> empRepository = null;
        private ICustomRepository<Consumption> empRepository2 = null; 
        public FlightController()
        {
            this.empRepository = new MyCustomRepository<Flight>();
            this.empRepository2 = new MyCustomRepository<Consumption>();
        }

         
        // GET: Flight
        public ActionResult Index(string thisFilter, string searchString, int? page)
        {
           // var photomodel = new PhotoViewModel();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = thisFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var Flights = from emp in empRepository.GetAllData()
                            select emp;
            if (!String.IsNullOrEmpty(searchString))
            {
                Flights = Flights.Where(emp => emp.FlightName.ToUpper().Contains(searchString.ToUpper()));
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(Flights.ToPagedList(pageNumber, pageSize));

           // return View(Flights);
        }

       
        public ActionResult Create()
        {
            return View(new Models.Flight());
        }

        [HttpPost]
        public ActionResult Create(Flight emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empRepository.InsertRecord(emp);
                    empRepository.SaveRecord();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save record.");
            }
            return View(emp);
        }

        public ActionResult Edit(int? id)
        {
            var flight = empRepository.SelectDataById(id);
            if (flight != null )
            { 
              ViewBag.DestinationCode = flight.DestinationCode;
              ViewBag.DepartureCode = flight.DepartureCode;
              ViewBag.DepartureCode = flight.DepartureCode;
            }
            else
            {
                return RedirectToAction("Index");
            }
         
            return View(flight);
        }

        [HttpPost]
        public ActionResult Edit(Flight emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    empRepository.Update(emp);
                    empRepository.SaveRecord();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to edit Flight record.");
            }

            return View(emp);
        }

        public ActionResult Details(int? id)
        {

            if (id != null)
            {
                return View(empRepository.SelectDataById(id)); 
            }
            else
            { 
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            return View(empRepository.SelectDataById(id));
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFlight(int id)
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

        public ActionResult LoadReport()
        {
               var Consumptions = from emp2 in empRepository2.GetAllData()
                          select emp2;
            SummaryReport rpt = new SummaryReport();
            List<SummaryRep> model = new List<SummaryRep>();
            model  = (List<SummaryRep>)AutoMapper.Mapper.Map(Consumptions,model , typeof(IEnumerable<Consumption>), typeof(List<SummaryRep>) );
            rpt.Load();
            rpt.SetDataSource(model);
            DataSet ds = new DataSet();
             
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(s, "application/pdf");


        }
    }
}