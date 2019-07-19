using InfoWeather.Logic;
using InfoWeather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}