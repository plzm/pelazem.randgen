using System;
using pelazem.rndgen;

namespace pelazem.rndgen.console
{
	class Program
	{
		private static Category<int>[] _passenger_counts =
		{
			new Category<int>(){Value = 1, RelativeWeight = 18},
			new Category<int>(){Value = 2, RelativeWeight = 15},
			new Category<int>(){Value = 3, RelativeWeight = 13},
			new Category<int>(){Value = 4, RelativeWeight = 9},
			new Category<int>(){Value = 5, RelativeWeight = 5},
			new Category<int>(){Value = 6, RelativeWeight = 2},
			new Category<int>(){Value = 7, RelativeWeight = 6},
			new Category<int>(){Value = 8, RelativeWeight =7},
			new Category<int>(){Value = 9, RelativeWeight = 8},
			new Category<int>(){Value = 10, RelativeWeight = 9}
		};

		static void Main(string[] args)
		{
			for (int i = 1; i <= 100; i++)
			{
				int cnt = RandomGenerator.Categorical.GetCategorical<int>(_passenger_counts);

				Console.WriteLine(cnt.ToString());
			}

			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
