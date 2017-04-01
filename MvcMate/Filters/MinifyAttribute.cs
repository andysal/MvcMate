using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web;
using System.Diagnostics.Contracts;

namespace MvcMate.Web.Mvc
{
    /// <summary>
    /// Represents an attribute that is used to compress or minify the http response
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class MinifyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.MinifyAttribute class.
        /// </summary>
        public MinifyAttribute()
        {
            ApplyCompression = false;
            RemoveWhiteSpace = false;
        }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.MinifyAttribute class.
        /// </summary>
        /// <param name="applyCompression">If set to true, the filter will compress the http response</param>
        /// <param name="removeWhiteSpace">If set to true, the filter will minify the http response</param>
        public MinifyAttribute(bool applyCompression, bool removeWhiteSpace)
        {
            Contract.Ensures(this.ApplyCompression == applyCompression);
            Contract.Ensures(this.RemoveWhiteSpace == removeWhiteSpace);

            ApplyCompression = applyCompression;
            RemoveWhiteSpace = removeWhiteSpace;
        }

        /// <summary>
        /// Gets or sets the flag that specify whether to apply compression to the http response
        /// </summary>
        public bool ApplyCompression { get; set; }

        /// <summary>
        /// Gets or sets the flag that specify whether to minify to the http response
        /// </summary>
        public bool RemoveWhiteSpace { get; set; }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="context">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Contract.EndContractBlock();

            if (!this.ApplyCompression)
            {
                base.OnActionExecuting(context);
            }

            HttpRequestBase request = context.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding))
            {
                return;
            }
            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = context.HttpContext.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action result executes.
        /// </summary>
        /// <param name="context">The filter context.</param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Contract.EndContractBlock();

            base.OnResultExecuted(context);

            if (this.RemoveWhiteSpace)
            {
                context.HttpContext.Response.Filter = new WhitespaceFilter(context.HttpContext.Response.Filter);
            }
        }

        internal class WhitespaceFilter : Stream
        {
            private static readonly Regex RegexBetweenTags = new Regex(@">\s+<", RegexOptions.Compiled);
            private static readonly Regex RegexLineBreaks = new Regex(@"\n\s+", RegexOptions.Compiled);

            private readonly Stream sink;

            public WhitespaceFilter(Stream sink)
            {
                this.sink = sink;
            }

            public override void Flush()
            {
                sink.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return sink.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                sink.SetLength(value);
            }

            public override void Close()
            {
                sink.Close();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return sink.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                var data = new byte[count];
                Buffer.BlockCopy(buffer, offset, data, 0, count);
                string html = Encoding.Default.GetString(buffer);


                html = RegexBetweenTags.Replace(html, "> <");
                html = RegexLineBreaks.Replace(html, string.Empty);
                html = html.Replace("\r", string.Empty);
                html = html.Replace("//<![CDATA[", string.Empty);
                html = html.Replace("//]]>", string.Empty);
                html = html.Replace("\n", string.Empty);

                byte[] outdata = Encoding.Default.GetBytes(html.Trim());
                sink.Write(outdata, 0, outdata.GetLength(0));
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override long Length
            {
                get { return 0; }
            }

            public override long Position { get; set; }
        }
    }
}
