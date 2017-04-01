﻿using System;
using System.IO;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using MvcMate.Web.Mvc;

namespace MvcMate.Web.Mvc.Tests
{
    [TestClass]
    public class Atom10ResultTestFixture
    {
        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_pattern_parameter_is_null()
        {
            Executing.This(() => new Atom10Result(null))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("feed");
        }

        [TestMethod]
        public void Ctor_should_set_Feed_property_if_feed_parameter_has_a_correct_value()
        {
            var feed = new SyndicationFeed();
            var result = new Atom10Result(feed);
            Assert.AreEqual<SyndicationFeed>(feed, result.Feed);
        }

        [TestMethod]
        public void Execute_should_throw_ArgumentNullException_if_context_parameter_is_null()
        {
            var feed = new SyndicationFeed();
            var result = new Atom10Result(feed);
            Executing.This(() => result.ExecuteResult(null))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("context");
        }

        [TestMethod]
        public void ExecuteResult_should_return_atom_mimetype()
        {
            var writer = new StringWriter();
            var responseMock = new Mock<HttpResponseBase>();
            responseMock.SetupProperty<string>(x => x.ContentType);
            responseMock.SetupGet(x => x.Output).Returns(writer);
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.SetupGet(x => x.Response).Returns(responseMock.Object);
            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext).Returns(httpContextMock.Object);


            var feed = new SyndicationFeed();
            var result = new Atom10Result(feed);            
            result.ExecuteResult(controllerContextMock.Object);

            Assert.AreEqual<string>("application/atom+xml", controllerContextMock.Object.HttpContext.Response.ContentType);
        }
    }
}
