using AspNetCoreSample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSample
{
	public class BrainstormSession
	{
		public int ID { get; set; }
		public DateTime DateCreated { get; set; }
		public string Name { get; set; }

		public List<Idea> Ideas { get; set; }
		public int IdeaCount { get; set; }


	}
}
