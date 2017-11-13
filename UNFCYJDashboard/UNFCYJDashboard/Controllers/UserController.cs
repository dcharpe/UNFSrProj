using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;
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

        // GET: /User/Dashboard

        public ActionResult Dashboard()
        {
            var query = dc.Admins.ToList().Select(p => new Admin
            {
                //each attribute data that will be display
                UserId = p.UserId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Password = p.Password

            });
            return View(query);
        }

        // GET: /User/Register

        public ActionResult Register()
        {
            
            return View();
            //cannot retain person's email
            /*if (System.Web.HttpContext.Current.Session["Email"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }*/
        }

        // POST: /User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        //ViewBag.Message = u.FirstName + " " + u.LastName + " has successfully registered!";
                  
                    }
            }
            return View(u);
        }

        // GET: /User/Login
        public ActionResult Login()
        {

            return View();
        }
        
        //POST: /USER/LOGIN
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin u)
        {
            if (ModelState.IsValid)
            {
                using (CYJDashEntities dc = new CYJDashEntities())
                {
                    var lg = dc.Admins.Where(e => e.Email.Equals(u.Email) && e.Password.Equals(u.Password)).FirstOrDefault();
                    if (lg != null)
                    {
                        return RedirectToAction("Register", "User");
                    }
                    else
                    {
                        Response.Write("<script> alsert('Invalid password')</script>");
                    }
                }
            }
            return View(u);
        }
    }
}