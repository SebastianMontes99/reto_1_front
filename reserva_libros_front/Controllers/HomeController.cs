using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reserva_libros_front.ServiceReference1;
using reserva_libros_front.Models;

namespace reserva_libros_front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }

    public class AuthController : Controller
    {
        private readonly Service1Client _wcfServiceClient = new Service1Client();

  
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                bool isAuthenticated = _wcfServiceClient.AuthenticateUser(model.Username, model.Password);

                if (isAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Credenciales inválidas";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error durante la autenticación: " + ex.Message;
                return View(model);
            }
        }
    }

    public class LibrosController : Controller
    {
        private readonly Service1Client _wcfServiceClient = new Service1Client();

        public ActionResult viewBook()
        {
            return View();
        }

        [HttpGet] 
        public ActionResult Book()
        {
            Libro[] librosArray = _wcfServiceClient.ObtenerLibros();
            List<Libro> librosList = librosArray.ToList();
            return View(librosList);
        }
    }



}