using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreSample.Controllers
{
    public class ApiController : ControllerBase
    {

		private readonly IBrainstormSessionRepository _sessionRepository;
		public ApiController(IBrainstormSessionRepository sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

		[HttpGet("forsession/{sessionId}")]
		public async Task<IActionResult> ForSession(int sessionId)
		{
			var session = await _sessionRepository.GetByIdAsync(sessionId);
			if (session == null)
			{
				return NotFound(sessionId);
			}

			var result = session.Ideas.Select(idea => new IdeaDTO()
			{
				Id = idea.Id,
				Name = idea.Name,
				Description = idea.Description,
				DateCreated = idea.DateCreated
			}).ToList();

			return Ok(result);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var session = await _sessionRepository.GetByIdAsync(model.SessionId);
			if (session == null)
			{
				return NotFound(model.SessionId);
			}

			var idea = new Idea()
			{
				DateCreated = DateTimeOffset.Now,
				Description = model.Description,
				Name = model.Name
			};
			

			await _sessionRepository.UpdateAsync(session);

			return Ok(session);
		}

		[HttpGet]
		public IActionResult GetDeviceSpecificData()
		{
			 var userAgent = this.HttpContext.Request.Headers["User-Agent"];
			var chromeHeader = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.45 Safari/535.19";
			if (userAgent.Equals(chromeHeader))
			{
				return Ok("Chrome from Android Mobile");
			}
			else
			{
				return Ok("Chrome from Desktop");
			}
		}
	}
}
