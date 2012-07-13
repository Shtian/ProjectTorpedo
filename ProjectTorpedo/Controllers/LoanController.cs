using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectTorpedo.Models;

namespace ProjectTorpedo.Controllers
{
    public class LoanController : Controller
    {
        private TorpedoContext db = new TorpedoContext();

        //
        // GET: /Loan/

        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.Owner).Include(l => l.Game);
            return View(loans.ToList());
        }

        //
        // GET: /Loan/Details/5

        public ActionResult Details(int id = 0)
        {
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        //
        // GET: /Loan/Create

        public ActionResult Create()
        {
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            return View();
        }

        //
        // POST: /Loan/Create

        [HttpPost]
        public ActionResult Create(Loan loan)
        {
            // Get and set the current user as the owner of the game/loan
            var currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
            if(currentUser != null)
            {
                loan.OwnerId = (Guid) currentUser.ProviderUserKey; // Every user _must_ have a GUID.
            }else
            {
                Trace.TraceError("Could not find user: " +User.Identity.Name);
            }

            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", loan.GameId);
            return View(loan);
        }

        //
        // GET: /Loan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "UserId", "Username", loan.OwnerId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", loan.GameId);
            return View(loan);
        }

        //
        // POST: /Loan/Edit/5

        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "UserId", "Username", loan.OwnerId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", loan.GameId);
            return View(loan);
        }

        //
        // GET: /Loan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        //
        // POST: /Loan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}