using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace InfoWeather.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [Display(Name = "Browse Files")]
        public HttpPostedFileBase[] Files { get; set; }
    }
}