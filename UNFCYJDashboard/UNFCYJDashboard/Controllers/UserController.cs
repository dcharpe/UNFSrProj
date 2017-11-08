using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNFCYJDashboard.Models;

namespace UNFCYJDashboard.Controllers
{
    public class UserController : Controller
    {

        //instantiates an object from the database
        CYJDashEntities dc = new CYJDashEntities();

        //displays list of records in the database 
        public ActionResult Index()
        {
            //for every attribute in listed the following code will turn it into a list
            var query = dc.Admins.ToList().Select(p => new Admin
            {
                //each attribute data that will be display
                UserId = p.UserId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Password = p.Password
            });
            //return a complete record to the index view
            return View(query);
        }


        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin u)
        {
            if (ModelState.IsValid)
            {
                using (CYJDashEntities dc = new CYJDashEntities())
                {
                    dc.Admins.Add(u);
                    dc.SaveChanges();
                    ModelState.Clear();
                    u = null;
                    ViewBag.Message = u.FirstName + " " + u.LastName + " has successfully registered!";
                }
            }
            return View(u);
        }
    }
}