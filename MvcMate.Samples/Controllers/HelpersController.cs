using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMate.Samples.Models.Helpers;

namespace MvcMate.Samples.Controllers
{
    public class HelpersController : Controller
    {
        //
        // GET: /Helpers/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            var model = new UploadViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult UploadFor()
        {
            var model = new UploadForViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult UploadForInNested()
        {
            var model = new UploadForInNestedViewModel();
            model.Child = new UploadForInNestedViewModel.ChildViewModel();
            return View(model);
        }
    }
}
