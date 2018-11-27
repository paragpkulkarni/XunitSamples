using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class CodeCoverageTest
    {
		public CodeCoverageTest()
		{

		}

		[Fact]
		public void SomeTest()
		{
			var obj = new CodeCoverageExample();
			obj.Number1 = 1;
			obj.Number2 = 1;
			obj.Number3 = 1;
			obj.Number4 = 1;
			obj.Number5 = 1;
			obj.Number6 = 1;
			obj.Number7 = 1;
			obj.Number8 = 1;
			obj.Number9 = 1;
			Assert.True(obj.Number1 == 1);
			Assert.True(obj.Number2 == 1);
			Assert.True(obj.Number3 == 1);
			Assert.True(obj.Number4 == 1);
			Assert.True(obj.Number5 == 1);
			Assert.True(obj.Number6 == 1);
			Assert.True(obj.Number7 == 1);
			Assert.True(obj.Number8 == 1);
			Assert.True(obj.Number9 == 1);

			//var sum = obj.CalculateSum();
			//Assert.True(sum == 9);
		}
    }
}
