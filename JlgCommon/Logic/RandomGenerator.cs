using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Logic
{
    public class RandomGenerator
    {
        private Random _rnd = new Random();


        public int GetRandomIntBetween(int lowestInclusive, int largestExclusive)
        {
            return _rnd.Next(lowestInclusive, largestExclusive);
        }

        public string GetRandomString(int length)
        {
            var randomString = new StringBuilder(10);
            for (int i = 0; i < length; i++)
            {
                randomString.Append((char)GetRandomIntBetween(97, 123));
            }
            return randomString.ToString();
        }

        public List<int> GetRandomIntList(int length, int minIntInclusive = 0, int maxIntExclusive = 100)
        {
            var randList = new List<int>();
            for (int i = 0; i < length; i++)
            {
                randList.Add(GetRandomIntBetween(minIntInclusive, maxIntExclusive));
            }
            return randList;
        }

        public List<Guid> GetRandomGuidList(int length)
        {
            var randList = new List<Guid>();
            for (int i = 0; i < length; i++)
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
            return new DateTime(year, month, day);
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
