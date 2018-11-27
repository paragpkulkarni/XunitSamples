using AspNetCoreSample;
using AspNetCoreSample.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
	public class MvcControllerTests
	{

		[Fact]
		public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
		{
			// Arrange
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			mockRepo.Setup(repo => repo.ListAsync())
				.ReturnsAsync(GetTestSessions());
			var controller = new HomeController(mockRepo.Object);

			// Act
			var result = await controller.Index();


			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<BrainstormSession>>(
				viewResult.ViewData.Model);
			Assert.Equal(2, model.Count());
		}


		[Fact]
		public async Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
		{
			// Arrange
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			mockRepo.Setup(repo => repo.ListAsync())
				.ReturnsAsync(GetTestSessions());
			var controller = new HomeController(mockRepo.Object);
			controller.ModelState.AddModelError("SessionName", "Required");
			var newSession = new HomeController.NewSessionModel();

			// Act
			var result = await controller.Index(newSession);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.IsType<SerializableError>(badRequestResult.Value);
		}

		[Fact]
		public async Task IndexPost_ReturnsARedirectAndAddsSession_WhenModelStateIsValid()
		{
			// Arrange
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			mockRepo.Setup(repo => repo.AddAsync(It.IsAny<BrainstormSession>()))
				.Returns(Task.CompletedTask)
				.Verifiable();
			var controller = new HomeController(mockRepo.Object);
			var newSession = new HomeController.NewSessionModel()
			{
				SessionName = "Test Name"
			};

			// Act
			var result = await controller.Index(newSession);

			// Assert
			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Null(redirectToActionResult.ControllerName);
			Assert.Equal("Index", redirectToActionResult.ActionName);
			mockRepo.Verify();
		}

		private List<BrainstormSession> GetTestSessions()
		{
			return new List<BrainstormSession>()
			{
				new BrainstormSession(){ID=1, IdeaCount=1,DateCreated=DateTime.Now.AddDays(-2)},
				new BrainstormSession(){ID=1, IdeaCount=2,DateCreated=DateTime.Now.AddDays(-1)},				
			};
		}
	}
}
