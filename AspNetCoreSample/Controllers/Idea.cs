using System;

namespace AspNetCoreSample.Controllers
{
	public class Idea
	{
		public Idea()
		{
		}

		public int Id { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public object Description { get; set; }
		public object Name { get; set; }
	}
}