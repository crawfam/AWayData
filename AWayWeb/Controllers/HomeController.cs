using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AWayData;
using AWayWeb.DataClasses;
using AWayWeb.Models;

namespace AWayWeb.Controllers
{
    public class HomeController : Controller
    {
        IAWayRepository _repo;

        public HomeController(IAWayRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            Random r = new Random();
            List<vwPhotoData> model = _repo.GetRandomPhotoData(46);
            BookText bt1 = _repo.getRandomBookText();
            ViewData.Add("book1", bt1);
            BookText bt2 = _repo.getRandomBookText();
            ViewData.Add("book2", bt2);
            ViewBag.Date = "Date";
            ViewBag.r1 = r.Next(24, 27);
            ViewBag.r2 = r.Next(34, 36);

            return View(model);
        }

        //public ActionResult Day(string date)
        public ActionResult Day(string date)
        {
            if (date != null)
            {
                DateTime dt = DateTime.Parse(date);
                ViewBag.Date = dt.ToShortDateString();
                List<Photo> model = _repo.GetPhotosByDate(dt);
                return View(model);
            }
            ViewBag.Date = "Date";
            return View("Index", null);
        }

        public ActionResult Week(string date)
        {
            if (date != null)
            {
                DateTime dt = DateTime.Parse(date);
                ViewBag.Date = dt.ToShortDateString();
                List<Photo> model = _repo.GetPhotosByDate(dt);                
                return View(model);
            }

            return RedirectToAction("Index", "Home");
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
