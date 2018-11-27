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
	public class ApiControllerTest
	{

		[Fact]
		public async Task Create_ReturnsBadRequest_GivenInvalidModel()
		{
			// Arrange & Act
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			var controller = new ApiController(mockRepo.Object);
			controller.ModelState.AddModelError("error", "some error");

			// Act
			var result = await controller.Create(model: null);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task ForSession_ReturnsHttpNotFound_ForInvalidSession()
		{
			// Arrange
			int testSessionId = 123;
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
				.ReturnsAsync((BrainstormSession)null);
			var controller = new ApiController(mockRepo.Object);

			// Act
			var result = await controller.ForSession(testSessionId);

			// Assert
			var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal(testSessionId, notFoundObjectResult.Value);
		}


		[Fact]
		public async Task ForSession_ReturnsIdeasForSession()
		{
			// Arrange
			int testSessionId = 123;
			var mockRepo = new Mock<IBrainstormSessionRepository>();
			mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
				.ReturnsAsync(GetTestSession());
			var controller = new ApiController(mockRepo.Object);

			// Act
			var result = await controller.ForSession(testSessionId);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnValue = Assert.IsType<List<IdeaDTO>>(okResult.Value);
			var idea = returnValue.FirstOrDefault();
			Assert.Equal("One", idea.Name);
		}

		private BrainstormSession GetTestSession()
		{
			var testSession = new BrainstormSession()
			{
				Ideas = new List<Idea>()
				{
				new Idea()
				{
					Id=1,
					DateCreated=DateTime.Now,
					Description="some idea for tes",
					Name="One"
				}
				}
			};
			return testSession;
		}
	}
}
