using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Razor;

namespace MvcMate.Samples.App_Start
{
    public class PreApplicationStart
    {
        public static void InitializeApplication()
        {
            WebCodeRazorHost.AddGlobalImport("MvcMate.Web.Mvc.Html");
        }
    }
}