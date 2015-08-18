using System;
using System.Collections.Generic;
using System.Linq;

namespace JlgCommon.Logic
{
    public class Normalizator
    {
        public List<double> NormalizeValues(List<double> values, double lowerLimit = 0, double upperLimit = 1)
        {
            var normalizedValues = new List<double>();
            var maxYMinusMinY = upperLimit - lowerLimit;

            var minValue = values.Min();
            var maxValue = values.Max();
            var maxValueMinusMinValue = maxValue - minValue;
            if (maxValueMinusMinValue == 0)
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (maxValue == 0)
                    {
                        normalizedValues.Add(lowerLimit);
                    }
                    else
                    {
                        normalizedValues.Add(upperLimit);
                    }
                }
            }
            else
            {
                for (var i = 0; i < values.Count; i++)
                {
                    var valueNormalized = Math.Round((lowerLimit + (values[i] - minValue) * maxYMinusMinY / maxValueMinusMinValue), 9);
                    normalizedValues.Add(valueNormalized);
                }
            }

            return normalizedValues;
        }

        public List<double?> NormalizeValues(List<double?> values, double? lowerLimit = null, double? upperLimit = null)
        {
            var normalizedValues = new List<double?>();

            if (!lowerLimit.HasValue)
            {
                lowerLimit = 0;
            }

            if (!upperLimit.HasValue)
            {
                upperLimit = 1;
            }
            var maxYMinusMinY = upperLimit - lowerLimit;

            double? minValue = null;
            double? maxValue = null;

            var valuesNotNull = values.Where(x => x.HasValue).ToList();
            if (valuesNotNull.Count > 0)
            {
                minValue = valuesNotNull.Min(x => x.Value);
                maxValue = valuesNotNull.Max(x => x.Value);
            }

            double? maxValueMinusMinValue = maxValue - minValue;

            if (maxValueMinusMinValue == 0)
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (maxValue == 0)
                    {
                        normalizedValues.Add(minValue);
                    }
                    else
                    {
                        normalizedValues.Add(maxValue);
                    }
                }
            }
            else
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (values[i].HasValue)
                    {
                        var valueNormalized = Math.Round((lowerLimit.Value + (values[i].Value - minValue.Value) * maxYMinusMinY.Value / maxValueMinusMinValue.Value), 9);
                        normalizedValues.Add(valueNormalized);
                    }
                    else
                    {
                        normalizedValues.Add(null);
                    }
                }
            }

            return normalizedValues;
        }

    }
}
