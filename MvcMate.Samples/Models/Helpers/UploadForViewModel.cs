using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMate.Samples.Models.Helpers
{
    public class UploadForViewModel
    {
        public HttpPostedFileBase TheFile { get; set; }

        [Required]
        public HttpPostedFileBase TheRequiredFile { get; set; }
    }
}