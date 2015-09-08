package logic;

import extensions.*;
import java.util.*;
import java.util.stream.Stream;

public class Normalizator {
    public List<Double> normalizeValues(List<Double> values, double lowerLimit, double upperLimit)
    {
        List<Double> normalizedValues = new ArrayList<Double>();
        double maxYMinusMinY = upperLimit - lowerLimit;

        Comparator<Double> comparer = (v1, v2) -> Double.compare( v1, v2 );
        double minValue = values.stream().min(comparer).get();
        double maxValue = values.stream().max(comparer).get();
        double maxValueMinusMinValue = maxValue - minValue;
        if (maxValueMinusMinValue == 0) {
            for (int i = 0; i < values.size(); i++) {
                if (maxValue == 0) {
                    normalizedValues.add(lowerLimit);
                }
                else {
                    normalizedValues.add(upperLimit);
                }
            }
        }
        else {
            for (int i = 0; i < values.size(); i++) {
                double valueNormalized = DoubleExtensions.roundToDecimals((lowerLimit + (values.get(i) - minValue) * maxYMinusMinY / maxValueMinusMinValue),9);
                normalizedValues.add(valueNormalized);
            }
        }
        return normalizedValues;
    }
}
