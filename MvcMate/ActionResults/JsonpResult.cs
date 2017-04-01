using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MvcMate.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to send JSON-formatted content to the response.
    /// </summary>
    public class JsonpResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.JsonpResult class.
        /// </summary>
        /// <param name="data">The object to be serialized.</param>
        public JsonpResult(object data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            Contract.Ensures(this.Data == data);
            Contract.EndContractBlock();

            Data = data;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the System.Web.Mvc.ActionResult class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="System.ArgumentNullException">The context parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Contract.EndContractBlock();

            var serializer = new JsonSerializer();
            var callbackName = context.HttpContext.Request["callback"];
            var jsonp = serializer.ToJsonpString(this.Data, callbackName);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(jsonp);
        }
    }
}
