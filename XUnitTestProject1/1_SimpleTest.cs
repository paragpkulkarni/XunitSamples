
using ClassLibrary1;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject1
{
	// Custom Test Collection
	[Collection("Our Test Collection #1")]
	// Put all test classes into a single test collection by default:
	[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
	// Set the maximum number of threads to use when running test in parallel:
	[assembly: CollectionBehavior(MaxParallelThreads = 2)]

	// Turn off parallelism inside the assembly: Default: false
	[assembly: CollectionBehavior(DisableTestParallelization = true)]
	public class UnitTest : IDisposable
	{
		private readonly ITestOutputHelper output;

		
		public UnitTest(ITestOutputHelper output)
		{
			this.output = output;		
			Console.WriteLine("ctor is called");
		}

		[Fact]
		public void AddTest()
		{
			var obj = new Class1();
			var result = obj.Add(3, 2);
			Assert.True(result == 5);

		}

		// Dispose the resources
		public void Dispose()
		{
			
		}

		[Theory]

		[InlineData(3, 2)]
		[InlineData(5, 7)]
		[InlineData(6, 7)]
		public void TestMultipleCombinations(int value1, int value2)
		{
			var obj = new Class1();
			var result = obj.Add(value1, value2);
			output.WriteLine($"Debug text Running the test for {value1} & {value2}");
			Assert.True(result == value1 + value2);
		}


	}

}
