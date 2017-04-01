using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;

namespace MvcMate.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to render an image by using an System.Drawing.Image instance
    /// </summary>
    public class ImageResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the image
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Gets or sets the desired mime type for the image
        /// </summary>
        /// <remarks>Supported values are: "image/gif", "image/jpeg", "image/tiff"</remarks>
        public string ImageMimeType { get; private set; }

        /// <summary>
        /// Gets or sets the image size
        /// </summary>
        public Size Thumbnailsize { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.ImageResult class.
        /// </summary>
        /// <param name="image">The image to be sent to the http user agent</param>
        /// <param name="mimeType">The mime type to be sent to the http user agent</param>
        public ImageResult(Image image, string mimeType)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            if (string.IsNullOrWhiteSpace(mimeType))
                throw new ArgumentNullException("mimeType");
            if (!(mimeType == "image/gif" || mimeType == "image/jpeg" || mimeType == "image/tiff"))
                throw new ArgumentException("The specified MIME type is not supported", "mimeType");
            Contract.Ensures(this.Image == image, "image");
            Contract.Ensures(this.ImageMimeType == mimeType, "mimeType");
            Contract.Ensures(this.Thumbnailsize == image.Size, "size");
            Contract.EndContractBlock();

            this.Image = image;
            this.ImageMimeType = mimeType;
            this.Thumbnailsize = Image.Size;
        }

        /// <summary>
        /// Initializes a new instance of the MvcMate.Web.Mvc.ImageResult class.
        /// </summary>
        /// <param name="image">The image to be sent to the http user agent</param>
        /// <param name="mimeType">The mime type to be sent to the http user agent</param>
        /// <param name="thumbnailSize">The size of the thumbnail to be sent to the http user agent</param>
        public ImageResult(Image image, string mimeType, Size thumbnailSize)
            : this(image, mimeType)
        {
            Contract.Ensures(this.Thumbnailsize == thumbnailSize, "imageSize");

            this.Thumbnailsize = thumbnailSize;
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

            var finalBitmap = new Bitmap(Image, this.Thumbnailsize);
            context.HttpContext.Response.ContentType = ImageMimeType;
            finalBitmap.Save(context.HttpContext.Response.OutputStream, GetImageFormat());
        }

        private ImageFormat GetImageFormat()
        {
            switch (this.ImageMimeType)
            {
                case "image/gif":
                    return ImageFormat.Gif;
                case "image/jpeg":
                    return ImageFormat.Jpeg;
                case "image/tiff":
                    return ImageFormat.Tiff;
                default:
                    throw new InvalidOperationException("The selected image format is not supported.");
            }
        }
    }
}
