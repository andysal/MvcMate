using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using MvcMate.Web.Mvc;
using System.Drawing;

namespace MvcMate.Samples.Controllers
{
    public class ActionResultsController : Controller
    {
        //
        // GET: /ActionResults/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Atom10()
        {
            var feed = BuildFeed();
            return this.Atom10(feed);
        }

        public ActionResult Image()
        {
            var image = new Bitmap(100, 100);
            for (int y = 0; y < 100; y++ )
            {
                for (int x = 0; x < 100; x++)
                {
                    image.SetPixel(x, y, Color.Black);
                }
            }
            return this.Image(image, "image/jpeg");
        }

        public ActionResult Jsonp()
        {
            var data = new { FirstName = "Martin", LastName = "Gore"};
            return this.Jsonp(data);
        }

        public ActionResult Rss20()
        {
            var feed = BuildFeed();
            return this.Rss20(feed);
        }

        private SyndicationFeed BuildFeed()
        {
            var items = new SyndicationItem[]
            {
                new SyndicationItem("First item", "Lorem ipsum 1", null),
                new SyndicationItem("Second item", "Lorem ipsum 2", null),
            };
            var feed = new SyndicationFeed("Fake Feed", "A fake feed for testing", new Uri("http://feeds.feedburner.com/ManagedDesignsNewsIT"), items);
            return feed;
        }
    }
}
