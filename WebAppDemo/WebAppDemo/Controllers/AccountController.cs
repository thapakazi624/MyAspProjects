using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppDemo.Models;
using System.Web.Security;

namespace WebAppDemo.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public  ActionResult LogIn(Models.Membership model)
        {
            using (var context =new OfficeDbEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
            
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using(var context = new OfficeDbEntities())
            {
                context.User.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("LogIn");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}