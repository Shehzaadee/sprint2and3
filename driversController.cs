using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GateBoys.Models;
using IdentitySample.Models;
using GateBoys.Helpers;

namespace GateBoys.Controllers
{
    public class driversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: drivers
        public ActionResult Index()
        {
            return View(db.drivers.ToList());
        }

        // GET: drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            driver driver = db.drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: drivers/Create
        public ActionResult Create(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.employeeEmail = email;
            return View();
        }

        // POST: drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,employeeName,employeeMidName,employeeSurname,employeeIdNumber,employeeEmail,employeePhonenumber" +
            ",employeeProfilePic,employeeCv,employeeIdDocument,employeeQualification,country,street_number,route,administrative_area_level_1,locality," +
            "postal_code,fullAdress,isStillEmployeed,reasonLeft,addedByEmail,dateRegistered")] driver driver, HttpPostedFileBase profPic, HttpPostedFileBase qualification, HttpPostedFileBase idDoc, HttpPostedFileBase cvDoc)
        {
            driver.employeeCv = uploader.getFileByte(cvDoc);
            driver.employeeIdDocument = uploader.getFileByte(idDoc);
            driver.employeeProfilePic = uploader.getFileByte(profPic);
            driver.dateRegistered = DateTime.Now.ToString();
            driver.isStillEmployeed = true;
            if (qualification!=null)
            {
                driver.employeeQualification = uploader.getFileByte(qualification);
            }
            if (ModelState.IsValid)
            {
                db.drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driver);
        }

        // GET: drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            driver driver = db.drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,employeeName,employeeMidName,employeeSurname,employeeIdNumber,employeeEmail,employeePhonenumber,employeeProfilePic,employeeCv,employeeIdDocument,employeeQualification,country,street_number,route,administrative_area_level_1,locality,postal_code,fullAdress,isStillEmployeed,reasonLeft,addedByEmail,dateRegistered")] driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            driver driver = db.drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            driver driver = db.drivers.Find(id);
            db.drivers.Remove(driver);
            db.SaveChanges();
            return RedirectToAction("Index");
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
