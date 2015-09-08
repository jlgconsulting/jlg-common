using System;
using System.Collections.Generic;
using System.Text;

namespace JlgCommon.Logic
{
    public class RandomGenerator
    {
        private Random _rnd = new Random();


        public int GetRandomIntBetween(int lowestInclusive, int largestExclusive)
        {
            return _rnd.Next(lowestInclusive, largestExclusive);
        }

        public string GetRandomString(int size)
        {
            var randomString = new StringBuilder(10);
            for (int i = 0; i < size; i++)
            {
                randomString.Append((char)GetRandomIntBetween(97, 123));
            }
            return randomString.ToString();
        }

        public List<int> GetRandomIntList(int size, int minInclusive = 0, int maxExclusive = 100)
        {
            var randList = new List<int>();
            for (int i = 0; i < size; i++)
            {
                randList.Add(GetRandomIntBetween(minInclusive, maxExclusive));
            }
            return randList;
        }

        public List<int?> GetRandomNullableIntList(int size, int minInclusive = 0, int maxExclusive = 100)
        {
            var randList = new List<int?>();
            for (int i = 0; i < size; i++)
            {
                var randomNr=GetRandomIntBetween(1, 70);
                if (randomNr % 3 == 0)
                {
                    randList.Add(null);
                }
                else
                {
                    var randomInt = GetRandomIntBetween(minInclusive, maxExclusive);
                    randList.Add(randomInt);
                }
            }
            return randList;
        }

        public List<double> GetRandomDoubleList(int size, int minInclusive = 0, int maxExclusive = 100)
        {
            var randList = new List<double>();
            double one = 1;
            for (int i = 0; i < size; i++)
            {
                double randomDouble = GetRandomIntBetween(minInclusive, maxExclusive) + (one / GetRandomIntBetween(2, 50));
                randList.Add(randomDouble);
            }
            return randList;
        }

        public List<double?> GetRandomNullableDoubleList(int size, int minInclusive = 0, int maxExclusive = 100)
        {
            var randList = new List<double?>();
            double one = 1;
            for (int i = 0; i < size; i++)
            {                
                var randomNr=GetRandomIntBetween(1, 70);
                if (randomNr % 3 == 0)
                {
                    randList.Add(null);
                }
                else
                {
                    double randomDouble = GetRandomIntBetween(minInclusive, maxExclusive) + (one / GetRandomIntBetween(2, 50));
                    randList.Add(randomDouble);
                }
                
            }
            return randList;
        }
        
        public List<Guid> GetRandomGuidList(int size)
        {
            var randList = new List<Guid>();
            for (int i = 0; i < size; i++)
            {
                randList.Add(Guid.NewGuid());
            }
            return randList;
        }
        
        public DateTime GetRandomDateTime(int startYearInclusive, int endYearExclusive)
        {
            var year = GetRandomIntBetween(startYearInclusive, endYearExclusive);
            var month = GetRandomIntBetween(1, 13);
            var day = GetRandomIntBetween(1, 29);
            var hour = GetRandomIntBetween(0, 25);
            var minute = GetRandomIntBetween(0, 60);
            var second = GetRandomIntBetween(0, 60);
            var millisecond = GetRandomIntBetween(0, 1000);

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public List<DateTime> GetRandomDateTimeList(int size, int startYearInclusive, int endYearExclusive)
        {
            var randList = new List<DateTime>();
            for (int i = 0; i < size; i++)
            {
                var randDateTime = GetRandomDateTime(startYearInclusive, endYearExclusive);
                randList.Add(randDateTime);
            }
            return randList;
        }

        public TimeSpan GetRandomTimeSpan()
        {

            var hours = GetRandomIntBetween(0, 24);
            var minutes = GetRandomIntBetween(0, 60);
            var seconds = GetRandomIntBetween(1, 60);

            var timespan = new TimeSpan(hours, minutes, seconds);
            return timespan;
        }
    }
}
