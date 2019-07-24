using InfoWeather.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace InfoWeather.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {
            if (ModelState.IsValid)
            {
                int fileCounter = 0;
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        if (file.FileName.EndsWith(".xlsx"))
                        {
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + Guid.NewGuid());
                            file.SaveAs(ServerSavePath);
                            DataBaseHandler.FilesToDB(ServerSavePath);
                            System.IO.File.Delete(ServerSavePath);
                            fileCounter++;
                        }
                    }
                }

                ViewBag.UploadStatus = fileCounter + " files uploaded successfully.";
            }
            return View();
        }
        public ActionResult ListArchives(int? Page, int? Year, int? Month)
        {
            int[] years = DataBaseHandler.GetYears();
            ViewBag.Years = new SelectList(years);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            StaticPagedList<WeatherEntry> fullData;
            int pageSize = 50;
            int pageNumber = (Page ?? 1);
            if (Year != null)
            {
                ViewBag.Year = Year;
                if (Month != null)
                {
                    ViewBag.Month = Month;
                    fullData = DataBaseHandler.GetData(pageNumber, pageSize, (int)Year, (int)Month);
                }
                else
                {
                    fullData = DataBaseHandler.GetData(pageNumber, pageSize, (int)Year);
                }
            }
            else
            {
                fullData = DataBaseHandler.GetData(pageNumber, pageSize);
            }
            return View(fullData);
        }
    }
}