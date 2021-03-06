﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace BerlinClock
{
	public class BerlinClockTimeConverterTests
	{
		[TestFixture]
		public class ConvertSeconds
		{
			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_Negative()
			{
				BerlinClockTimeConverter.SecondsRows(-1);
			}

			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_MoreThan59()
			{
				BerlinClockTimeConverter.SecondsRows(60);
			}

			[TestCase(new[] {"Y"}, 0)]
			[TestCase(new[] {"O"}, 1)]
			[TestCase(new[] {"Y"}, 2)]
			[TestCase(new[] {"Y"}, 58)]
			[TestCase(new[] {"O"}, 59)]
			public void YellowLampBlinksEveryOtherSecond(string[] expected, int seconds)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.SecondsRows(seconds), $"converting {seconds} seconds");
			}
		}

		[TestFixture]
		public class ConvertHours
		{
			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_Negative()
			{
				BerlinClockTimeConverter.HoursRows(-1);
			}

			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_MoreThan25()
			{
				BerlinClockTimeConverter.HoursRows(25);
			}

			[Test]
			public void GeneratesTwoRowsOfLamp()
			{
				Assert.AreEqual(2, BerlinClockTimeConverter.HoursRows(0).Length);
			}

			[TestCase("OOOO", 0)]
			[TestCase("OOOO", 1)]
			[TestCase("OOOO", 4)]
			[TestCase("ROOO", 5)]
			[TestCase("RROO", 13)]
			[TestCase("RRRO", 15)]
			[TestCase("RRRR", 24)]
			public void FirstRow_EachLampIs5Hours(string expected, int hours)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.HoursRows(hours)[0],
					$"converting {hours} hours");
			}

			[TestCase("OOOO", 0)]
			[TestCase("ROOO", 1)]
			[TestCase("RRRR", 4)]
			[TestCase("OOOO", 5)]
			[TestCase("RRRO", 13)]
			[TestCase("OOOO", 15)]
			[TestCase("RRRR", 24)]
			public void SecondRow_EachLampIs1Hour(string expected, int hours)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.HoursRows(hours).Last(), $"converting {hours} hours");
			}
		}

		[TestFixture]
		public class ConvertMinutes
		{
			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_Negative()
			{
				BerlinClockTimeConverter.SecondsRows(-1);
			}

			[Test]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void Fails_MoreThan59()
			{
				BerlinClockTimeConverter.SecondsRows(60);
			}

			[Test]
			public void GeneratesTwoRowsOfLamp()
			{
				Assert.AreEqual(2, BerlinClockTimeConverter.MinutesRows(0).Length);
			}

			[Test]
			public void FirstRowHas11Lamps()
			{
				Assert.AreEqual(11, BerlinClockTimeConverter.MinutesRows(0).First().Length);
			}

			[Test]
			public void SecondRowHas4Lamps()
			{
				Assert.AreEqual(4, BerlinClockTimeConverter.MinutesRows(0).Last().Length);
			}

			[TestCase("OOOOOOOOOOO", 0)]
			[TestCase("OOOOOOOOOOO", 1)]
			[TestCase("OOOOOOOOOOO", 4)]
			[TestCase("YOOOOOOOOOO", 5)]
			[TestCase("YOOOOOOOOOO", 6)]
			[TestCase("YYOOOOOOOOO", 12)]
			[TestCase("YYOOOOOOOOO", 13)]
			[TestCase("YYOOOOOOOOO", 14)]
			public void FirstRow_EachLamp_Is5Minutes(string expected, int minutes)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.MinutesRows(minutes)[0],
					$"converting {minutes} minutes");
			}

			[TestCase("YYROOOOOOOO", 15)]
			[TestCase("YYROOOOOOOO", 16)]
			[TestCase("YYRYOOOOOOO", 24)]
			[TestCase("YYRYYOOOOOO", 25)]
			[TestCase("YYRYYROOOOO", 30)]
			[TestCase("YYRYYRYYROO", 45)]
			[TestCase("YYRYYRYYRYY", 59)]
			public void FirstRow_EveryQuarter_IsRedLamp(string expected, int minutes)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.MinutesRows(minutes)[0],
					$"converting {minutes} minutes");
			}

			[TestCase("OOOO", 0)]
			[TestCase("YOOO", 1)]
			[TestCase("YYYY", 4)]
			[TestCase("OOOO", 5)]
			[TestCase("YOOO", 6)]
			[TestCase("YYOO", 12)]
			[TestCase("YYYO", 13)]
			[TestCase("YYYY", 14)]
			[TestCase("OOOO", 15)]
			[TestCase("YOOO", 16)]
			[TestCase("YYYY", 24)]
			[TestCase("OOOO", 25)]
			[TestCase("YYYY", 59)]
			public void SecondRow_EachLampIs1Minute(string expected, int minutes)
			{
				Assert.AreEqual(expected, BerlinClockTimeConverter.MinutesRows(minutes).Last(), $"converting {minutes} minutes");
			}
		}
	}
}