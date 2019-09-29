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
using Microsoft.AspNet.Identity;

namespace GateBoys.Controllers
{
    public class wishlistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: wishlists
        public ActionResult Index()
        {
            return View(db.wishlists.ToList());
        }
        // GET: wishlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        public ActionResult remove(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var user = User.Identity.GetUserName();
            wishlist wishlist = db.wishlists.FirstOrDefault(a => a.id == id && a.usermail == user);
            if (wishlist == null)
            {
                return RedirectToAction("Index");
            }
            db.wishlists.Remove(wishlist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult clearMyWish()
        {
            var user = User.Identity.GetUserName();
            var wishlists = db.wishlists.Where(a => a.usermail == user).ToList();
            if (wishlists == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in wishlists)
                {
                    db.wishlists.Remove(item);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        // GET: wishlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: wishlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productId,productName,price,usermail,date")] wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.wishlists.Add(wishlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wishlist);
        }

        // GET: wishlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: wishlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productId,productName,price,usermail,date")] wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishlist);
        }

        // GET: wishlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wishlist wishlist = db.wishlists.Find(id);
            db.wishlists.Remove(wishlist);
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
