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
    public class JsonpResultTestFixture
    {
        [TestMethod]
        public void Ctor_should_throw_ArgumentNullException_if_data_parameter_is_null()
        {
            Executing.This(() => new JsonpResult(null))
                .Should()
                .Throw<ArgumentNullException>()
                .And
                .ValueOf
                .ParamName
                .Should()
                .Be
                .EqualTo("data");
        }

        [TestMethod]
        public void Ctor_should_set_Data_property_if_data_parameter_has_a_correct_value()
        {
            var data = "101";
            var result = new JsonpResult(data);
            Assert.AreEqual(data, result.Data);
        }

        [TestMethod]
        public void ExecuteResult_should_throw_ArgumentNullException_if_context_parameter_is_null()
        {
            var data = "101";
            var result = new JsonpResult(data);
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
        public void ExecuteResult_should_return_json_mimetype()
        {
            var writer = new StringWriter();
            var responseMock = new Mock<HttpResponseBase>();
            responseMock.SetupProperty<string>(x => x.ContentType);
            responseMock.SetupGet(x => x.Output).Returns(writer);

            var requestMock = new Mock<HttpRequestBase>();
            requestMock.SetupGet(x => x["callback"]).Returns("fake");

            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.SetupGet(x => x.Request).Returns(requestMock.Object);
            httpContextMock.SetupGet(x => x.Response).Returns(responseMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(x => x.HttpContext).Returns(httpContextMock.Object);

            var data = "101";
            var result = new JsonpResult(data);
            result.ExecuteResult(controllerContextMock.Object);

            Assert.AreEqual<string>("application/json", controllerContextMock.Object.HttpContext.Response.ContentType);
        }
    }
}
