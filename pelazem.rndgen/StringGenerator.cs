using System;
using System.Text;
using pelazem.util;

namespace pelazem.rndgen
{
	public class StringGenerator : GeneratorBase<string>
	{
		private int _charMin = 32;
		private int _charMax = 126;

		private int _defaultMinLength = 1;
		private int _defaultMaxLength = 50;

		public StringGenerator()
		{
		}

		public StringGenerator(int defaultMinLength, int defaultMaxLength)
		{
			_defaultMinLength = defaultMinLength;
			_defaultMaxLength = defaultMaxLength;
		}

		private int GetLength(int minLength, int maxLength)
		{
			int result = Converter.GetInt32(RandomGenerator.Numeric.GetUniform(minLength, maxLength));
			return result;
		}

		public string GetString()
		{
			if (this.UseEmpty()) return this.EmptyValue;

			int length = GetLength(_defaultMinLength, _defaultMaxLength);

			return GetString(length);
		}

		public string GetString(int length)
		{
			if (this.UseEmpty()) return this.EmptyValue;

			StringBuilder sb = new StringBuilder(length);

			for (int i = 1; i <= length; i++)
				sb.Append((char)Converter.GetInt32(RandomGenerator.Numeric.GetUniform(_charMin, _charMax)));

			return sb.ToString();
		}

		public string GetString(int minLength, int maxLength)
		{
			if (this.UseEmpty()) return this.EmptyValue;

			int length = GetLength(minLength, maxLength);

			return GetString(length);
		}
	}
}
