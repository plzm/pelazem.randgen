using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pelazem.util;

namespace pelazem.rndgen
{
	public class CategoricalGenerator : GeneratorBase<object>
	{
		public CategoricalGenerator() { }

		public object GetCategorical(IList categoricalValues)
		{
			if (categoricalValues == null || categoricalValues.Count == 0 || this.UseEmpty())
				return this.EmptyValue;

			int index = GetIndex(0, categoricalValues.Count);

			return categoricalValues[index];
		}

		public T GetCategorical<T>(IEnumerable<T> categoricalValues)
		{
			int count = categoricalValues.Count();

			if (categoricalValues == null || count == 0 || this.UseEmpty())
				return default(T);

			int index = GetIndex(0, count);

			return categoricalValues.ElementAt(index);
		}

		/// <summary>
		/// This method returns one of the allowable values from a list.
		/// This lets you randomly choose a value from a list where some values should be more, or less, probable to be chosen than others.
		/// Note: this method incurs extra processing cost due to internal sorting and weighting operations.
		/// The list of allowable values should not exceed approx. 200 million items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="weightedCategoricalValues">List of tuples where the first value is the value, and the second value is the relative weight of the value.</param>
		/// <returns></returns>
		public T GetCategorical<T>(IList<Category<T>> weightedCategoricalValues)
		{
			if (weightedCategoricalValues == null || weightedCategoricalValues.Count == 0)
				return default(T);

			// If all values have a weight of zero, no need for weighting arithmetic
			if (!weightedCategoricalValues.Any(v => v.RelativeWeight != 0))
				return GetCategorical<T>(weightedCategoricalValues.Select(w => w.Value));


			List<Category<T>> normalizedValues = GetNormalizedWeightedCategoricalValues(weightedCategoricalValues);

			int index = GetIndex(0, normalizedValues.Last().RangeMax + 1);

			Category<T> pick = normalizedValues.FirstOrDefault(v => v.RangeMin <= index && index <= v.RangeMax);

			if (pick == null)
				return default(T);
			else
				return pick.Value;
		}

		private int GetIndex(int minValue, int maxValue)
		{
			return Converter.GetInt32(RandomGenerator.Numeric.Generator.GetUniform(minValue, maxValue));
		}

		private List<Category<T>> GetNormalizedWeightedCategoricalValues<T>(IEnumerable<Category<T>> weightedCategoricalValues)
		{
			if (weightedCategoricalValues == null || weightedCategoricalValues.Count() == 0)
				return new List<Category<T>>();

			List<Category<T>> values = weightedCategoricalValues.ToList();

			// Sort the allowable values by weight
			values.Sort((a, b) => { return a.RelativeWeight.CompareTo(b.RelativeWeight); });

			// If they're all equal weights, no further action needed
			if (values.First().RelativeWeight == values.Last().RelativeWeight)
				return values;


			// Eliminate zero weights - hey, you pass me stuff with zero weights as well as non-zero weights, I'll assume you want the zeroed stuff not counted :)
			int index = values.IndexOf(values.First(c => c.RelativeWeight != 0.0));

			values.RemoveRange(0, index);

			// Get the minimum non-zero weight; we'll use this to proportionalize values. We'll boost it to exaggerate small weight differences.
			double normalizerWeight = values.First().RelativeWeight / 10.0;

			// Set each value's weighted count
			values.AsParallel().ForAll(v => v.WeightedCount = Converter.GetInt32(v.RelativeWeight / normalizerWeight));

			// Set each allowable value's range min and max in accordance with its weighted value
			// This is to allow random selection
			int rangeCounter = 0;

			foreach (Category<T> category in values)
			{
				category.RangeMin = rangeCounter;
				category.RangeMax = category.RangeMin + category.WeightedCount - 1;
				rangeCounter += category.RangeMax + 1;
			}

			return values;
		}
	}

	public class Category<T>
	{
		private double _relativeWeight = 0.0;

		public T Value { get; set; }

		public double RelativeWeight
		{
			get
			{
				return _relativeWeight;
			}
			set
			{
				_relativeWeight = Math.Abs(value);
			}
		}

		internal int WeightedCount { get; set; }
		internal int RangeMin { get; set; }
		internal int RangeMax { get; set; }
	}
}
