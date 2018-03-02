using System;
using pelazem.util;

namespace hoser.Generators.Random
{
	public class BooleanGenerator : GeneratorBase<bool>
	{
		public BooleanGenerator() { }

		public bool GetBoolean()
		{
			if (this.UseEmpty()) return this.EmptyValue;

			return	((Converter.GetInt32(RandomGenerator.Numeric.GetUniform(1000000, 2000000)) % 2) == 0);
		}
	}
}
