using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreSample.Models;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSample.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBrainstormSessionRepository _sessionRepository;


		public HomeController(IBrainstormSessionRepository sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

		public async Task<IActionResult> Index()
		{
			var sessionList = await _sessionRepository.ListAsync();

			var model = sessionList.Select(session => new BrainstormSession()
			{
				ID = session.ID,
				DateCreated = session.DateCreated,
				Name = session.Name,
			});

			return View(model);
		}

		public class NewSessionModel
		{
			[Required]
			public string SessionName { get; set; }
		}

		[HttpPost]
		public async Task<IActionResult> Index(NewSessionModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			else
			{
				await _sessionRepository.AddAsync(new BrainstormSession()
				{
					DateCreated = DateTime.Now,
					Name = model.SessionName
				});
			}

			return RedirectToAction(actionName: nameof(Index));
		}
	}


}

