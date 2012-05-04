using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectTorpedo.Models;

namespace ProjectTorpedo.Controllers
{
    public class AutocompleteController : Controller
    {
        private TorpedoContext db = new TorpedoContext();

        // GET: /Autocomplete/Users

        public JsonResult Users(string term)
        {
            // Get all users that start with the term and exclude the currently logged in user.
            var users =
                db.Users.Where(usr => (usr.Username.StartsWith(term) && usr.Username != User.Identity.Name)) 
                .Select(usr => new { usr.UserId, value = usr.Username })
                .Take(5)
                .ToArray();

            Trace.TraceInformation("Number of users:" +users.Count());
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        // GET: /Autocomplete/Games

        public JsonResult Games(string term)
        {
            // Get all users that start with the term and exclude the currently logged in user.
            var games =
                db.Games.Where(game => game.Name.StartsWith(term))
                .Select(usr => new { usr.Id, value = usr.Name })
                .Take(5)
                .ToArray();
            return Json(games, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
