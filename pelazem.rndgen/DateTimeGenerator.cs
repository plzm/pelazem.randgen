using System;
using System.Collections.Generic;
using System.Text;
using pelazem.util;

namespace hoser.Generators.Random
{
	public class DateTimeGenerator : GeneratorBase<DateTime>
	{
		public static TimeSpan DefaultTimeSpan { get; set; } = new TimeSpan(7, 0, 0, 0);

		public DateTimeGenerator()
		{

		}

		public DateTime GetDateTime()
		{
			if (this.UseEmpty()) return this.EmptyValue;

			return GetDateTime(DateTime.UtcNow.Subtract(DefaultTimeSpan), DateTime.UtcNow.Add(DefaultTimeSpan));
		}

		public DateTime GetDateTime(DateTime start, DateTime end)
		{
			if (this.UseEmpty()) return this.EmptyValue;

			if (start == end)
				return start;

			DateTime dtEnd = (end >= start ? end : start);

			long diffTicks = dtEnd.Subtract(start).Ticks;

			double value = RandomGenerator.Numeric.GetUniform(0, diffTicks);

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(value))
					value = RandomGenerator.Numeric.GetUniform(0, diffTicks);

				this.UniqueValues.Add(value, false);
			}

			DateTime result = new DateTime(start.Ticks + Converter.GetInt64(value), (dtEnd.Kind != DateTimeKind.Unspecified ? dtEnd.Kind : DateTimeKind.Utc));

			return result;
		}

		public TimeSpan GetTimeSpan()
		{
			return GetTimeSpan(TimeSpan.MinValue, DefaultTimeSpan);
		}

		public TimeSpan GetTimeSpan(TimeSpan minTimeSpan, TimeSpan maxTimeSpan)
		{
			long diff = maxTimeSpan.Subtract(minTimeSpan).Ticks;

			long random = Converter.GetInt64(RandomGenerator.Numeric.GetUniform(0, Converter.GetDouble(diff)));

			TimeSpan rnd = new TimeSpan(random);

			return minTimeSpan.Add(rnd);
		}
	}
}
