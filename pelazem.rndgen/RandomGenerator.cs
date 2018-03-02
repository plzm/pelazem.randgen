using System;
using System.Collections.Generic;
using System.Text;
using pelazem.util;

namespace pelazem.rndgen
{
	public static class RandomGenerator
	{

		private static BooleanGenerator _booleanGenerator = null;
		private static CategoricalGenerator _categoricalGenerator = null;
		private static DateTimeGenerator _dateTimeGenerator = null;
		private static NumericGenerator _numericGenerator = null;
		private static StringGenerator _stringGenerator = null;

		public static BooleanGenerator Boolean
		{
			get
			{
				if (_booleanGenerator == null)
					_booleanGenerator = new BooleanGenerator();

				return _booleanGenerator;
			}
		}

		public static CategoricalGenerator Categorical
		{
			get
			{
				if (_categoricalGenerator == null)
					_categoricalGenerator = new CategoricalGenerator();

				return _categoricalGenerator;
			}
		}

		public static DateTimeGenerator DateTime
		{
			get
			{
				if (_dateTimeGenerator == null)
					_dateTimeGenerator = new DateTimeGenerator();

				return _dateTimeGenerator;
			}
		}

		public static NumericGenerator Numeric
		{
			get
			{
				if (_numericGenerator == null)
					_numericGenerator = new NumericGenerator();

				return _numericGenerator;
			}
		}

		public static StringGenerator String
		{
			get
			{
				if (_stringGenerator == null)
					_stringGenerator = new StringGenerator();

				return _stringGenerator;
			}
		}
	}
}
