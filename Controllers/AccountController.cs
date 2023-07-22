using Lab_DataPassing.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Lab_DataPassing.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Login(LoginViewModel model) 
        
        {
            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            /* if (!string.IsNullOrEmpty(model.Email))
             {
                 string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                             @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                 Regex re = new Regex(emailRegex);
                 if (!re.IsMatch(model.Email))
                 {
                     ModelState.AddModelError("Email", "Email is not valid");
                 }
             }*/
            if (string.IsNullOrEmpty(model.Username))
            {
                ModelState.AddModelError("Password", "Password is required");
            }
            if (ModelState.IsValid)
            { 
                if (model.Username == "user@gmail.com" && model.Password == "123456") 
                {
                    TempData["Message"] = "Welcome Back!";
                    string strUser = JsonSerializer.Serialize(model);//setting the session state variable
                    //Serializing  the model details and storing them in the Session variable to pass the data across the application. 
                    HttpContext.Session.SetString("User", strUser);
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Username or Password doesn't exist!";
                }
            }
            return View();
        }
        /*
        But we encounter an error –“ Session has not been configured for this application or request”. 
        ➢ To configure the session for this application, we need to add the Session middleware to the request pipeline.
        So to enable the session middleware, Program.cs must contain: 
        • A call to AddSession - Adds services required for application session state to the IServiceCollection
        • A call to UseSession - Adds the SessionMiddleware to automatically enable the session state for the application.
        The order of middleware is important. Call UseSession after UseRouting  Once the Session is configured ,HttpContext.Session is available to set the session.
       
         */

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View(); 
        }
    }
}
