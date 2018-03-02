using System;
using pelazem.rng;

namespace pelazem.rndgen
{
	public class NumericGenerator : GeneratorBase<double>
	{
		private RNG _rng = null;

		public RNG Generator
		{
			get
			{
				if (_rng == null)
					_rng = new RNG();

				return _rng;
			}
		}
	}
}
