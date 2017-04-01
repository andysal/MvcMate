using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Diagnostics.Contracts;

namespace MvcMate.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to send RSS-formatted content to the response.
    /// </summary>
    public class Rss20Result : ActionResult
    {
        /// <summary>
        /// Gets or sets the feed
        /// </summary>
        public SyndicationFeed Feed { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.Rss20Result class.
        /// </summary>
        /// <param name="feed">The feed to be serialized to RSS format</param>
        public Rss20Result(SyndicationFeed feed)
        {
            if (feed == null)
                throw new ArgumentNullException("feed");
            Contract.Ensures(this.Feed == feed);
            Contract.EndContractBlock();

            this.Feed = feed;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type 
        /// that inherits from the MvcMate.Web.Mvc.Atom10Result class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="System.ArgumentNullException">The context parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Contract.EndContractBlock();

            context.HttpContext.Response.ContentType = "application/rss+xml";

            var rssFormatter = new Rss20FeedFormatter(Feed);
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                rssFormatter.WriteTo(writer);
            }
        }

    }
}
