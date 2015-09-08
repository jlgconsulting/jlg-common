package tests.extensions;

import extensions.DateExtensions;
import extensions.DoubleExtensions;
import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class DoubleExtensionsTest {

    @Test
    public void testRoundToDecimals() {
        assertTrue(23 == DoubleExtensions.roundToDecimals(23.456, 0));
        assertTrue(24 == DoubleExtensions.roundToDecimals(23.56, 0));
        assertTrue(23.5 == DoubleExtensions.roundToDecimals(23.456, 1));
        assertTrue(23.4 == DoubleExtensions.roundToDecimals(23.436, 1));
        assertTrue(23.46 == DoubleExtensions.roundToDecimals(23.456, 2));
        assertTrue(23.45 == DoubleExtensions.roundToDecimals(23.452, 2));
        assertTrue(23.457 == DoubleExtensions.roundToDecimals(23.4568, 3));
        assertTrue(23.456 == DoubleExtensions.roundToDecimals(23.4563, 3));
        assertTrue(23.4563 == DoubleExtensions.roundToDecimals(23.4563, 4));
    }

    @Test
    public void testDifferenceInPercents() {
        double nr1 = 10;
        double nr2 = 15;
        assertTrue(50 == DoubleExtensions.differenceInPercents(nr2, nr1));
        double nr3 = 20;
        assertTrue(100 == DoubleExtensions.differenceInPercents(nr3, nr1));
        double nr4 = 5;
        assertTrue(-50 == DoubleExtensions.differenceInPercents(nr4, nr1));
        double nr5 = -5;
        assertTrue(-150 == DoubleExtensions.differenceInPercents(nr5, nr1));
    }

    @Test
    public void testPercentValue() {
        double nr1 = 10;
        assertTrue(5 == DoubleExtensions.percentValue(nr1, 50));
        double nr2 = 100;
        assertTrue(10 == DoubleExtensions.percentValue(nr2, 10));
        double nr3 = 50;
        assertTrue(5 == DoubleExtensions.percentValue(nr3, nr1));
        double nr4 = 70;
        assertTrue(3.64 == DoubleExtensions.percentValue(nr4, 5.2));
        double nr5 = 100;
        assertTrue(-20 == DoubleExtensions.percentValue(nr5, -20));
        double nr6 = 30;
        assertTrue(-0.6 == DoubleExtensions.percentValue(nr6, -2));
        double nr7 = 25.3;
        assertTrue(-1.8722 == DoubleExtensions.roundToDecimals(DoubleExtensions.percentValue(nr7, -7.4), 4));
    }

    @Test
    public void testIncreaseByPercent() {
        double nr1 = 10;
        assertTrue(15 == DoubleExtensions.increaseByPercent(nr1, 50));
        double nr2 = 100;
        assertTrue(102 == DoubleExtensions.increaseByPercent(nr2, 2));
        double nr3 = 50;
        assertTrue(55 == DoubleExtensions.increaseByPercent(nr3, 10));
        double nr4 = 70;
        assertTrue(73.64 == DoubleExtensions.increaseByPercent(nr4, 5.2));
        double nr5 = 100;
        assertTrue(80 == DoubleExtensions.increaseByPercent(nr5, -20));
        double nr6 = 30;
        assertTrue(29.4 == DoubleExtensions.increaseByPercent(nr6, -2));
        double nr7 = 25.3;
        assertTrue(23.4278 == DoubleExtensions.increaseByPercent(nr7, -7.4));
    }
}
