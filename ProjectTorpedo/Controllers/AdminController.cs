using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstMembershipSharp;
using ProjectTorpedo.Models;

namespace ProjectTorpedo.Controllers
{
    public class AdminController : Controller
    {
        private TorpedoContext context = new TorpedoContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            
                var model = context.Users.OrderBy(usrname => usrname.Username);

                return View(model.ToList());
            
            
        }
        public ActionResult Details(Guid id)
        {

                var user = context.Users.First(u => u.UserId == id);
                return View(user);

        }

        public ActionResult Roles()
        {

                var model = context.Roles.OrderBy(usrname => usrname.RoleName);

                return View(model);

        }

        public ActionResult CreateRole()
        {
            return View();
        }

        //
        // POST: /Loan/Create

        [HttpPost]
        public ActionResult CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                role.RoleId = Guid.NewGuid();
                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("Roles");
            }
            return View(role);
        }



        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }


    }
}
