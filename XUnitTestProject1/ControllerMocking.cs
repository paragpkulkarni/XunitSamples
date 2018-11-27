using AspNetCoreSample;
using AspNetCoreSample.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class ControllerMockingTest
    {
		public ControllerMockingTest()
		{

		}

		[Fact]
		public void GetDeviceSpecificDataTest()
		{
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			var controller = new ApiController(mockRepo.Object);
			controller.ControllerContext = new ControllerContext();
			controller.ControllerContext.HttpContext = new DefaultHttpContext();
			controller.ControllerContext.HttpContext.Request.Headers["User-Agent"] = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.45 Safari/535.19";
			var result = controller.GetDeviceSpecificData();
			Assert.NotNull(result);			

		}

	}
}
