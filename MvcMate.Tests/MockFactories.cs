using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace MvcMate.Web.Tests
{
    public class MockFactories
    {
        public static HtmlHelper CreateFakeHtmlHelper()
        {
            var vd = new ViewDataDictionary();
            var mockViewContext = new Mock<ViewContext>(
                                                    new ControllerContext(
                                                        new Mock<HttpContextBase>().Object,
                                                        new RouteData(),
                                                        new Mock<ControllerBase>().Object),
                                                    new Mock<IView>().Object,
                                                    vd,
                                                    new TempDataDictionary(),
                                                    new Mock<TextWriter>().Object);
            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer
                .Setup(v => v.ViewData)
                .Returns(vd);
            return new HtmlHelper(mockViewContext.Object, mockViewDataContainer.Object);
        }

        public static HtmlHelper<T> CreateFakeHtmlHelper<T>()
        {
            var vd = new ViewDataDictionary();
            var mockViewContext = new Mock<ViewContext>(
                                                    new ControllerContext(
                                                        new Mock<HttpContextBase>().Object,
                                                        new RouteData(),
                                                        new Mock<ControllerBase>().Object),
                                                    new Mock<IView>().Object,
                                                    vd,
                                                    new TempDataDictionary(),
                                                    new Mock<TextWriter>().Object);
            mockViewContext
                .Setup(c => c.ClientValidationEnabled)
                .Returns(true);
            mockViewContext
                .Setup(c => c.UnobtrusiveJavaScriptEnabled)
                .Returns(true);
            mockViewContext
                .Setup(c => c.FormContext)
                .Returns(new FormContext());
            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer
                .Setup(v => v.ViewData)
                .Returns(vd);
            return new HtmlHelper<T>(mockViewContext.Object, mockViewDataContainer.Object);
        }
    }
}
