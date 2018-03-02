using System;
using System.Collections.Generic;
using System.Text;

namespace hoser.Generators.Random
{
	public abstract class GeneratorBase<T>
	{
		protected double? _percentChanceEmpty = 0;
		protected T _emptyValue = default(T);


		/// <summary>
		/// Whether repeated values in the output should be prevented.
		/// </summary>
		public bool EnforceUniqueValues { get; protected set; } = false;

		protected internal SortedDictionary<object, bool> UniqueValues { get; }


		/// <summary>
		/// Percent chance that a generated value will be empty.
		/// Passing null or negative will set this to zero.
		/// Passing greater than 100 will set this to 100.
		/// </summary>
		public double? PercentChanceEmpty
		{
			get { return _percentChanceEmpty; }
			protected set
			{
				if (value == null || value < 0)
					_percentChanceEmpty = 0;
				else if (value >= 100)
					_percentChanceEmpty = 100;
				else
					_percentChanceEmpty = value;
			}
		}

		/// <summary>
		/// If an empty value is generated (based on PercentChanceEmpty), the value to be written to the output.
		/// </summary>
		public T EmptyValue
		{
			get { return _emptyValue; }
			protected set
			{
				if (value == null)
					_emptyValue = default(T);
				else
					_emptyValue = value;
			}
		}

		/// <summary>
		/// Should the next value to be generated be empty
		/// </summary>
		/// <returns></returns>
		public bool UseEmpty()
		{
			bool itsEmpty = false;

			if (this.PercentChanceEmpty == 100)
				itsEmpty = true;
			else if (this.PercentChanceEmpty > 0)
				itsEmpty = (RandomGenerator.Numeric.GetUniform(0, 100) <= this.PercentChanceEmpty);
			else
				itsEmpty = false;   // Just being explicit...

			return itsEmpty;
		}
	}
}
