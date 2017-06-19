using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using BLL.Entities;
using BLL.Interfaces;
using ClassGenerator;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private IService Service;


        public HomeController(IService Service)
        {
            this.Service = Service;
        }


        public ActionResult Index()
        {
            var Data = Service.GetProcessData();;
            return View(Data[0]);
        }

        public ActionResult Select()
        {

            

            return View();

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
}