using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal? DifferenceInPercents(this decimal nr1, decimal nr2)
        {
            if (Math.Abs(nr2) > 0)
            {
                return Math.Round((nr1 * 100 / nr2) - 100, 2);
            }
            else
            {
                return null;
            }
        }

        public static decimal? DifferenceInPercents(this decimal? nr1, decimal? nr2)
        {
            if (!nr1.HasValue || !nr2.HasValue)
            {
                return null;
            }

            if (Math.Abs(nr2.Value) > 0)
            {
                return Math.Round((nr1.Value * 100 / nr2.Value) - 100, 2);
            }
            else
            {
                return null;
            }
        }

        public static decimal IncreaseByPercent(this decimal nr, decimal percent)
        {           
            return nr + nr.PercentValue(percent);
        }

        public static decimal PercentValue(this decimal nr, decimal percent)
        {
            var percentValueFromNr = (percent / 100) * nr;
            return percentValueFromNr;
        }      

    }
}
