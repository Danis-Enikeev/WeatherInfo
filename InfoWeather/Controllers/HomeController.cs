using InfoWeather.Logic;
using InfoWeather.Models;
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
                        if (file.FileName.EndsWith(".xlsx")){
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + Guid.NewGuid());
                            file.SaveAs(ServerSavePath);
                            DataBaseHandler.FilesToDB(ServerSavePath);
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
            Dictionary<int, int[]> yearMonths = new Dictionary<int, int[]>();
            int[] years = DataBaseHandler.GetYears();
            foreach (int year in years)
            {
                int[] months = DataBaseHandler.GetMonths(year);
                yearMonths.Add(year, months);
            }
            ViewBag.Years = new SelectList(yearMonths.Keys);
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            List<WeatherEntry> fullData = new List<WeatherEntry>();
            int pageSize = 50;
            int pageNumber = (Page ?? 1);
            if (Year != null)
            {
                ViewBag.Year = Year;
                if (Month != null)
                {
                    ViewBag.Month = Month;
                    fullData = DataBaseHandler.GetData((int)Year, (int)Month);
                }
                else
                {
                    foreach (int month in yearMonths[(int)Year])
                    {
                        fullData.AddRange(DataBaseHandler.GetData((int)Year, month));
                    }
                }

            }
            else
            {
                foreach (int year in years)
                {
                    foreach (int month in yearMonths[year])
                    {
                        fullData.AddRange(DataBaseHandler.GetData(year, month));
                    }
                }
            }
            return View(fullData.ToPagedList(pageNumber, pageSize));
        }
    }
}