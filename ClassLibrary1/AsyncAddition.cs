using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
	public class AsyncArithmetic
	{

		public async Task<int> AddAync(int first, int second)
		{
			await Task.Delay(2000);
			return first + second;

		}
	}
}
