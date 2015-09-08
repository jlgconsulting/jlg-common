package tests.logic;

import extensions.DateExtensions;
import logic.RandomGenerator;
import org.junit.Before;
import org.junit.Test;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import static org.junit.Assert.*;

public class RandomGeneratorTest {

    private RandomGenerator randomGenerator;

    @Before
    public void setUp() {
        randomGenerator = new RandomGenerator();
    }

    @Test
    public void testGetRandomIntBetween() {
        for (int i = 0; i < 100; i++) {
            int randInt = randomGenerator.getRandomIntBetween(1342, 34520);
            assertTrue(randInt >= 1342 && randInt < 34520);
        }
    }

    @Test
    public void testGetRandomString() {
        for (int i = 0; i < 100; i++) {
            String randString = randomGenerator.getRandomString(30);
            assertTrue(randString.length() == 30);
        }
    }

    @Test
    public void testGetRandomIntList() {
        for (int i = 0; i < 100; i++) {
            List<Integer> randIntList = randomGenerator.getRandomIntList(20, -245, 700);
            assertTrue(randIntList.size() == 20);
            for (int randInt : randIntList) {
                assertTrue(randInt >= -245 && randInt < 700);
            }
        }
    }

    @Test
    public void testGetRandomDoubleList() {
        for (int i = 0; i < 70; i++) {
            List<Double> randDoubleList = randomGenerator.getRandomDoubleList(17, -3, 9);
            assertTrue(randDoubleList.size() == 17);
            for (double randDouble : randDoubleList) {
                assertTrue(randDouble >= -3 && randDouble < 9);
            }
        }
    }

    @Test
    public void testGetRandomUUIDList() {
        for (int i = 0; i < 100; i++) {
            List<UUID> randIntList = randomGenerator.getRandomUUIDList(15);
            assertTrue(randIntList.size() == 15);
        }
    }

    @Test
    public void testGetRandomDate() {
        Date first2002 = DateExtensions.createDate(2002, 1, 1);
        Date first2015 = DateExtensions.createDate(2015, 1, 1);
        for (int i = 0; i < 100; i++) {
            Date randDate = randomGenerator.getRandomDate(2002, 2015);
            assertTrue(randDate.after(first2002) && randDate.before(first2015));
        }
    }

    @Test
    public void testRandomDateList() {
        Date first1900 = DateExtensions.createDate(1900, 1, 1);
        Date first2250 = DateExtensions.createDate(2250, 1, 1);
        for (int i = 0; i < 50; i++) {
            List<Date> randDateList = randomGenerator.getRandomDateList(30, 1900, 2250);
            assertTrue(randDateList.size() == 30);
            for(Date randDate : randDateList) {
                assertTrue(randDate.after(first1900) && randDate.before(first2250));
            }
        }
    }
}