package tests.extensions;

import extensions.DateLegacyExtensions;
import org.junit.*;
import java.util.Calendar;
import java.util.Date;

import static org.junit.Assert.*;

public class DateLegacyExtensionsTest {

    private Date date9Feb2015;
    private Date date4March2014H22M15S13Ms789;
    @Before
    public void setUp() {
        date9Feb2015 = DateLegacyExtensions.createDate(2015, Calendar.FEBRUARY, 9);
        date4March2014H22M15S13Ms789 = DateLegacyExtensions.createDate(2014, Calendar.MARCH, 4, 22, 15, 13, 789);
    }

    @Test
    public void testCreateDate() {
        assertEquals(2015, DateLegacyExtensions.getYear(date9Feb2015));
        assertEquals(Calendar.FEBRUARY, DateLegacyExtensions.getMonth(date9Feb2015));
        assertEquals(9, DateLegacyExtensions.getDay(date9Feb2015));
    }

    @Test
    public void testAddDays() {
        Date tomorrow = DateLegacyExtensions.addDays(date9Feb2015, 1);
        assertEquals(10, DateLegacyExtensions.getDay(tomorrow));
        assertEquals(Calendar.FEBRUARY, DateLegacyExtensions.getMonth(tomorrow));

        Date twoDaysBefore = DateLegacyExtensions.addDays(date9Feb2015, -2);
        assertEquals(7, DateLegacyExtensions.getDay(twoDaysBefore));
        assertEquals(Calendar.FEBRUARY, DateLegacyExtensions.getMonth(twoDaysBefore));

        Date elevenDaysBefore = DateLegacyExtensions.addDays(date9Feb2015, -11);
        assertEquals(29, DateLegacyExtensions.getDay(elevenDaysBefore));
        assertEquals(Calendar.JANUARY, DateLegacyExtensions.getMonth(elevenDaysBefore));
    }

   @Test
    public void testAddMonths() {
       Date nextMonth = DateLegacyExtensions.addMonths(date9Feb2015, 1);
       assertEquals(Calendar.MARCH, DateLegacyExtensions.getMonth(nextMonth));
       assertEquals(2015, DateLegacyExtensions.getYear(nextMonth));
       assertEquals(9, DateLegacyExtensions.getDay(nextMonth));

       Date beforeFourMonths = DateLegacyExtensions.addMonths(date9Feb2015, -4);
       assertEquals(Calendar.OCTOBER, DateLegacyExtensions.getMonth(beforeFourMonths));
       assertEquals(2014, DateLegacyExtensions.getYear(beforeFourMonths));
       assertEquals(9, DateLegacyExtensions.getDay(nextMonth));
   }

    @Test
    public void testAddYears() {
        Date nextYear = DateLegacyExtensions.addYears(date9Feb2015, 1);
        assertEquals(2016, DateLegacyExtensions.getYear(nextYear));

        Date before5Years = DateLegacyExtensions.addYears(date9Feb2015, -5);
        assertEquals(2010, DateLegacyExtensions.getYear(before5Years));
    }

    @Test
    public void testAddHours() {
        Date nextHour = DateLegacyExtensions.addHours(date4March2014H22M15S13Ms789, 1);
        assertEquals(23, DateLegacyExtensions.getHour(nextHour));
        assertEquals(4, DateLegacyExtensions.getDay(nextHour));

        Date threeHoursBefore = DateLegacyExtensions.addHours(date4March2014H22M15S13Ms789, -3);
        assertEquals(19, DateLegacyExtensions.getHour(threeHoursBefore));

        Date fourHoursLater = DateLegacyExtensions.addHours(date4March2014H22M15S13Ms789, 4);
        assertEquals(2, DateLegacyExtensions.getHour(fourHoursLater));
        assertEquals(5, DateLegacyExtensions.getDay(fourHoursLater));
    }

    @Test
    public void testAddMinutes() {
        Date after5Minutes = DateLegacyExtensions.addMinutes(date4March2014H22M15S13Ms789, 5);
        assertEquals(20, DateLegacyExtensions.getMinute(after5Minutes));

        Date before3Minutes = DateLegacyExtensions.addMinutes(date4March2014H22M15S13Ms789, -3);
        assertEquals(12, DateLegacyExtensions.getMinute(before3Minutes));
    }

    @Test
    public void testAddSeconds() {
        Date after10Seconds = DateLegacyExtensions.addSeconds(date4March2014H22M15S13Ms789, 10);
        assertEquals(23, DateLegacyExtensions.getSecond(after10Seconds));

        Date before7Seconds = DateLegacyExtensions.addSeconds(date4March2014H22M15S13Ms789, -7);
        assertEquals(6, DateLegacyExtensions.getSecond(before7Seconds));
    }

    @Test
    public void testAddMilliseconds() {
        Date after20Milliseconds = DateLegacyExtensions.addMilliseconds(date4March2014H22M15S13Ms789, 20);
        assertEquals(809, DateLegacyExtensions.getMillisecond(after20Milliseconds));

        Date before10Milliseconds = DateLegacyExtensions.addMilliseconds(date4March2014H22M15S13Ms789, -10);
        assertEquals(779, DateLegacyExtensions.getMillisecond(before10Milliseconds));
    }

    @Test
    public void testSetDay() {
        Date newDate = DateLegacyExtensions.setDay(date9Feb2015, 12);
        assertEquals(12, DateLegacyExtensions.getDay(newDate));
    }

    @Test
    public void testSetMonth() {
        Date newDate = DateLegacyExtensions.setMonth(date9Feb2015, Calendar.JUNE);
        assertEquals(Calendar.JUNE, DateLegacyExtensions.getMonth(newDate));
    }

    @Test
      public void testSetYear() {
        Date newDate = DateLegacyExtensions.setYear(date9Feb2015, 2100);
        assertEquals(2100, DateLegacyExtensions.getYear(newDate));
    }

    @Test
    public void testSetHour() {
        Date newDate = DateLegacyExtensions.setHour(date9Feb2015, 20);
        assertEquals(20, DateLegacyExtensions.getHour(newDate));
    }

    @Test
    public void testSetMinute() {
        Date newDate = DateLegacyExtensions.setMinute(date9Feb2015, 58);
        assertEquals(58, DateLegacyExtensions.getMinute(newDate));
    }

    @Test
    public void testSetSecond() {
        Date newDate = DateLegacyExtensions.setSecond(date9Feb2015, 50);
        assertEquals(50, DateLegacyExtensions.getSecond(newDate));
    }

    @Test
    public void testSetMillisecond() {
        Date newDate = DateLegacyExtensions.setMillisecond(date9Feb2015, 980);
        assertEquals(980, DateLegacyExtensions.getMillisecond(newDate));
    }

    @Test
    public void testGetDay() {
        assertEquals(9, DateLegacyExtensions.getDay(date9Feb2015));
    }

    @Test
    public void testGetMonth() {
        assertEquals(Calendar.FEBRUARY, DateLegacyExtensions.getMonth(date9Feb2015));
    }

    @Test
    public void testGetYear() {
        assertEquals(2015, DateLegacyExtensions.getYear(date9Feb2015));
    }

    @Test
    public void testGetHour() {
        assertEquals(22, DateLegacyExtensions.getHour(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetMinute() {
        assertEquals(15, DateLegacyExtensions.getMinute(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetSecond() {
        assertEquals(13, DateLegacyExtensions.getSecond(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetMillisecond() {
        assertEquals(789, DateLegacyExtensions.getMillisecond(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testToMonthYearString() {
        assertEquals("Feb 2015", DateLegacyExtensions.toMonthYearString(date9Feb2015));
    }

    @Test
    public void testToMonthNameDayYearString() {
        assertEquals("Feb 09 2015", DateLegacyExtensions.toMonthNameDayYearString(date9Feb2015));
    }

    @Test
    public void testToDateIgnoringTime() {
        Date ignoringTime = DateLegacyExtensions.toDateIgnoringTime(date4March2014H22M15S13Ms789);
        assertEquals(2014, DateLegacyExtensions.getYear(ignoringTime));
        assertEquals(Calendar.MARCH, DateLegacyExtensions.getMonth(ignoringTime));
        assertEquals(4, DateLegacyExtensions.getDay(ignoringTime));

        assertEquals(0, DateLegacyExtensions.getHour(ignoringTime));
        assertEquals(0, DateLegacyExtensions.getMinute(ignoringTime));
        assertEquals(0, DateLegacyExtensions.getSecond(ignoringTime));
        assertEquals(0, DateLegacyExtensions.getMillisecond(ignoringTime));
    }

    @Test
    public void testToMonthYear() {
        Date ignoringDay = DateLegacyExtensions.toMonthYear(date4March2014H22M15S13Ms789);
        assertEquals(1, DateLegacyExtensions.getDay(ignoringDay));
        assertEquals(0, DateLegacyExtensions.getHour(ignoringDay));
    }

    @Test
    public void testDifferenceInMonths() {
        int difference1 = DateLegacyExtensions.differenceInMonths(date9Feb2015, date4March2014H22M15S13Ms789);
        assertEquals(11, difference1);

        int difference2 = DateLegacyExtensions.differenceInMonths(date4March2014H22M15S13Ms789, date9Feb2015);
        assertEquals(-11, difference2);
    }
}