using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSample
{
	public interface IBrainstormSessionRepository
	{
		Task<List<BrainstormSession>> ListAsync();
		Task AddAsync(BrainstormSession session);

		Task<BrainstormSession> GetByIdAsync(int ID);

		Task UpdateAsync(BrainstormSession session);
	}

	public class BrainStormRepository : IBrainstormSessionRepository
	{
		List<BrainstormSession> list = new List<BrainstormSession>();
		public async Task AddAsync(BrainstormSession session)
		{
			list.Add(session);
			await Task.Delay(500);
		}

		public async Task<BrainstormSession> GetByIdAsync(int ID)
		{
			return await Task.FromResult(new BrainstormSession()
			{
				ID = 1,
				DateCreated = DateTime.Now,
				IdeaCount = 2,
				Name = "First brain storming session"
			});

		}

		public async Task<List<BrainstormSession>> ListAsync()
		{
			return await Task.FromResult(list);
		}

		public async Task UpdateAsync(BrainstormSession session)
		{
			var index = list.FindIndex(s => s.ID == session.ID);
			if(index>-1)
			{
				var tempSession = list[index];
				tempSession.Name = session.Name;				
			}
			await Task.Delay(200);
		}

	}


}
