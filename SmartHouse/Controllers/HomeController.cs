using SmartHouse.DdAccess;
using SmartHouse.Models;
using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHouse.Controllers
{
    public class HomeController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: Home
        public ActionResult Index()
        {
            List<IDrawe> devices = new List<IDrawe>();
            devices.AddRange(db.Tvs);
            devices.AddRange(db.Fridges);
            devices.AddRange(db.Lamps);
            devices.AddRange(db.Cookers);
            devices.AddRange(db.Microwaves);

            ViewBag.SelectedDevice = Session["SelectedDevice"];
            ViewBag.DeviceConsole = Session["DeviceConsole"];
            ViewBag.MvcWebApi = Session["MvcWebApi"] == null ? "WebAPI" : Session["MvcWebApi"];

            return View(devices);
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: Home/Create/tv
        public ActionResult Create(string device)
        {
            switch (device)
            {
                case "Tv":
                    Tv tv = new Tv();
                    db.Tvs.Add(tv);
                    db.SaveChanges();
                    Session["SelectedDevice"] = tv;
                    Session["DeviceConsole"] = new DeviceConsole(tv);
                    break;
                case "Fridge":
                    Fridge fridge = new Fridge();
                    db.Fridges.Add(fridge);
                    db.SaveChanges();
                    Session["SelectedDevice"] = fridge;
                    Session["DeviceConsole"] = new DeviceConsole(fridge);
                    break;
                case "Lamp":
                    Lamp lamp = new Lamp();
                    db.Lamps.Add(lamp);
                    db.SaveChanges();
                    Session["SelectedDevice"] = lamp;
                    Session["DeviceConsole"] = new DeviceConsole(lamp);

                    break;
                case "Cooker":
                    Cooker cooker = new Cooker();
                    db.Cookers.Add(cooker);
                    db.SaveChanges();
                    Session["SelectedDevice"] = cooker;
                    Session["DeviceConsole"] = new DeviceConsole(cooker);

                    break;
                case "Microwave":
                    Microwave microwave = new Microwave();
                    db.Microwaves.Add(microwave);
                    db.SaveChanges();
                    Session["SelectedDevice"] = microwave;
                    Session["DeviceConsole"] = new DeviceConsole(microwave);
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5/tv
        public ActionResult Edit(int? id, string device)
        {
            switch (device)
            {
                case "Tv":
                    Session["SelectedDevice"] = db.Tvs.Find(id);
                    Session["DeviceConsole"] = new DeviceConsole(db.Tvs.Find(id));
                    break;
                case "Fridge":
                    Session["SelectedDevice"] = db.Fridges.Find(id);
                    Session["DeviceConsole"] = new DeviceConsole(db.Fridges.Find(id));
                    break;
                case "Lamp":
                    Session["SelectedDevice"] = db.Lamps.Find(id);
                    Session["DeviceConsole"] = new DeviceConsole(db.Lamps.Find(id));
                    break;
                case "Cooker":
                    Session["SelectedDevice"] = db.Cookers.Find(id);
                    Session["DeviceConsole"] = new DeviceConsole(db.Cookers.Find(id));
                    break;
                case "Microwave":
                    Session["SelectedDevice"] = db.Microwaves.Find(id);
                    Session["DeviceConsole"] = new DeviceConsole(db.Microwaves.Find(id));
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        // POST: Home/Edit/Tv
        [HttpPost]
        public ActionResult Edit(Device device)
        {
            db.Entry(device).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            Session["SelectedDevice"] = null;
            Session["DeviceConsole"] = null;
            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5/tv
        public ActionResult Delete(int? id, string device)
        {
            if (id == null)
            { 
                return HttpNotFound(); 
            }
            switch (device)
            {
                case "Tv":
                    Tv tv = db.Tvs.Find(id);
                    if (tv != null)
                    {
                        db.Tvs.Remove(tv);
                        db.SaveChanges();
                    }
                    break;
                case "Fridge":
                    Fridge fridge = db.Fridges.Find(id);
                    if (fridge != null)
                    {
                        db.Fridges.Remove(fridge);
                        db.SaveChanges();
                    }
                    break;
                case "Lamp":
                    Lamp lamp = db.Lamps.Find(id);
                    if (lamp != null)
                    {
                        db.Lamps.Remove(lamp);
                        db.SaveChanges();
                    }
                    break;
                case "Cooker":
                    Cooker cooker = db.Cookers.Find(id);
                    if (cooker != null)
                    {
                        db.Cookers.Remove(cooker);
                        db.SaveChanges();
                    }
                    break;
                case "Microwave":
                    Microwave microwave = db.Microwaves.Find(id);
                    if (microwave != null)
                    {
                        db.Microwaves.Remove(microwave);
                        db.SaveChanges();
                    }
                    break;
                default:
                    break;
            }
            Session["SelectedDevice"] = null;
            Session["DeviceConsole"] = null;
            return RedirectToAction("Index");
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                string str = collection.ToString();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Console/up
        public ActionResult Console(string command)
        {
            string str = command;
            switch (command)
            {
                case "Up":
                    (Session["DeviceConsole"] as DeviceConsole).PrevSetting();
                    break;
                case "Down":
                    (Session["DeviceConsole"] as DeviceConsole).NextSetting();
                    break;
                case "Left":
                    (Session["DeviceConsole"] as DeviceConsole).DecrValue();
                    break;
                case "Right":
                    (Session["DeviceConsole"] as DeviceConsole).IncrValue();
                    break;
                case "On":
                    (Session["SelectedDevice"] as Device).On();
                    break;
                case "Off":
                    (Session["SelectedDevice"] as Device).Off();
                    break;
                case "Ok":
                    db.Entry(Session["SelectedDevice"]).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Session["SelectedDevice"] = null;
                    Session["DeviceConsole"] = null;
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        // GET: Home/MvcWebApi/true
        public ActionResult MvcWebApi(string mvcWebApi)
        {
            if (mvcWebApi == "WebAPI")
            {
                Tv tv = (Session["SelectedDevice"] as Tv);
                if (tv != null)
                {
                    Session["SelectedDevice"] = null;
                    Session["DeviceConsole"] = null;
                    return RedirectToAction("Tv", "WebApiView", tv);
                }
                else
                {
                    return RedirectToAction("Index", "WebApiView");
                }
            }
            else
            {
                Session["MvcWebApi"] = "WebAPI";
                return RedirectToAction("Index");
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
