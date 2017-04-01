using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMate.Web.Mvc;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MvcMate.Web.Tests.ActionResults
{
    [TestClass]
    public class ImageResultTestFixture
    {
        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_image_parameter_is_null()
        {
            Executing.This(() => new ImageResult(null, "image/jpeg"))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("image");
        }

        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_mimeType_parameter_is_null()
        {
            var image = new Bitmap(100, 100);
            Executing.This(() => new ImageResult(image, null))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("mimeType");
        }

        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_mimeType_parameter_is_blank()
        {
            var image = new Bitmap(100, 100);
            Executing.This(() => new ImageResult(image, ""))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("mimeType");
        }

        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_mimeType_parameter_is_whitespace()
        {
            var image = new Bitmap(100, 100);
            Executing.This(() => new ImageResult(image, " "))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("mimeType");
        }

        [TestMethod]
        public void Ctor_should_throw_ArgumentException_if_mimeType_parameter_is_not_supported()
        {
            var image = new Bitmap(100, 100);
            Executing.This(() => new ImageResult(image, "image/fake"))
                .Should()
                .Throw<ArgumentException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("mimeType");
        }
    }
}
