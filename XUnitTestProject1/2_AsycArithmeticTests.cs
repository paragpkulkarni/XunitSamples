using ClassLibrary1;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
	public class AsyncArithmeticTests
	{
		[Fact]
		public async Task AddAyncTest()
		{
			var obj = new AsyncArithmetic();
			var sum = await obj.AddAync(3, 2);
			Assert.True(sum == 5);
		}

	}
}
