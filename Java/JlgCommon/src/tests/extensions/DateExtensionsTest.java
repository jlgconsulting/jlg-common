package tests.extensions;

import extensions.DateExtensions;
import org.junit.*;
import java.util.Calendar;
import java.util.Date;

import static org.junit.Assert.*;

public class DateExtensionsTest {

    private Date date9Feb2015;
    private Date date4March2014H22M15S13Ms789;
    @Before
    public void setUp() {
        date9Feb2015 = DateExtensions.createDate(2015, Calendar.FEBRUARY, 9);
        date4March2014H22M15S13Ms789 = DateExtensions.createDate(2014, Calendar.MARCH, 4, 22, 15, 13, 789);
    }

    @Test
    public void testCreateDate() {
        assertEquals(2015, DateExtensions.getYear(date9Feb2015));
        assertEquals(Calendar.FEBRUARY, DateExtensions.getMonth(date9Feb2015));
        assertEquals(9, DateExtensions.getDay(date9Feb2015));
    }

    @Test
    public void testAddDays() {
        Date tomorrow = DateExtensions.addDays(date9Feb2015, 1);
        assertEquals(10, DateExtensions.getDay(tomorrow));
        assertEquals(Calendar.FEBRUARY, DateExtensions.getMonth(tomorrow));

        Date twoDaysBefore = DateExtensions.addDays(date9Feb2015, -2);
        assertEquals(7, DateExtensions.getDay(twoDaysBefore));
        assertEquals(Calendar.FEBRUARY, DateExtensions.getMonth(twoDaysBefore));

        Date elevenDaysBefore = DateExtensions.addDays(date9Feb2015, -11);
        assertEquals(29, DateExtensions.getDay(elevenDaysBefore));
        assertEquals(Calendar.JANUARY, DateExtensions.getMonth(elevenDaysBefore));
    }

   @Test
    public void testAddMonths() {
       Date nextMonth = DateExtensions.addMonths(date9Feb2015, 1);
       assertEquals(Calendar.MARCH, DateExtensions.getMonth(nextMonth));
       assertEquals(2015, DateExtensions.getYear(nextMonth));
       assertEquals(9, DateExtensions.getDay(nextMonth));

       Date beforeFourMonths = DateExtensions.addMonths(date9Feb2015, -4);
       assertEquals(Calendar.OCTOBER, DateExtensions.getMonth(beforeFourMonths));
       assertEquals(2014, DateExtensions.getYear(beforeFourMonths));
       assertEquals(9, DateExtensions.getDay(nextMonth));
   }

    @Test
    public void testAddYears() {
        Date nextYear = DateExtensions.addYears(date9Feb2015, 1);
        assertEquals(2016, DateExtensions.getYear(nextYear));

        Date before5Years = DateExtensions.addYears(date9Feb2015, -5);
        assertEquals(2010, DateExtensions.getYear(before5Years));
    }

    @Test
    public void testAddHours() {
        Date nextHour = DateExtensions.addHours(date4March2014H22M15S13Ms789, 1);
        assertEquals(23, DateExtensions.getHour(nextHour));
        assertEquals(4, DateExtensions.getDay(nextHour));

        Date threeHoursBefore = DateExtensions.addHours(date4March2014H22M15S13Ms789, -3);
        assertEquals(19, DateExtensions.getHour(threeHoursBefore));

        Date fourHoursLater = DateExtensions.addHours(date4March2014H22M15S13Ms789, 4);
        assertEquals(2, DateExtensions.getHour(fourHoursLater));
        assertEquals(5, DateExtensions.getDay(fourHoursLater));
    }

    @Test
    public void testAddMinutes() {
        Date after5Minutes = DateExtensions.addMinutes(date4March2014H22M15S13Ms789, 5);
        assertEquals(20, DateExtensions.getMinute(after5Minutes));

        Date before3Minutes = DateExtensions.addMinutes(date4March2014H22M15S13Ms789, -3);
        assertEquals(12, DateExtensions.getMinute(before3Minutes));
    }

    @Test
    public void testAddSeconds() {
        Date after10Seconds = DateExtensions.addSeconds(date4March2014H22M15S13Ms789, 10);
        assertEquals(23, DateExtensions.getSecond(after10Seconds));

        Date before7Seconds = DateExtensions.addSeconds(date4March2014H22M15S13Ms789, -7);
        assertEquals(6, DateExtensions.getSecond(before7Seconds));
    }

    @Test
    public void testAddMilliseconds() {
        Date after20Milliseconds = DateExtensions.addMilliseconds(date4March2014H22M15S13Ms789, 20);
        assertEquals(809, DateExtensions.getMillisecond(after20Milliseconds));

        Date before10Milliseconds = DateExtensions.addMilliseconds(date4March2014H22M15S13Ms789, -10);
        assertEquals(779, DateExtensions.getMillisecond(before10Milliseconds));
    }

    @Test
    public void testSetDay() {
        Date newDate = DateExtensions.setDay(date9Feb2015, 12);
        assertEquals(12, DateExtensions.getDay(newDate));
    }

    @Test
    public void testSetMonth() {
        Date newDate = DateExtensions.setMonth(date9Feb2015, Calendar.JUNE);
        assertEquals(Calendar.JUNE, DateExtensions.getMonth(newDate));
    }

    @Test
      public void testSetYear() {
        Date newDate = DateExtensions.setYear(date9Feb2015, 2100);
        assertEquals(2100, DateExtensions.getYear(newDate));
    }

    @Test
    public void testSetHour() {
        Date newDate = DateExtensions.setHour(date9Feb2015, 20);
        assertEquals(20, DateExtensions.getHour(newDate));
    }

    @Test
    public void testSetMinute() {
        Date newDate = DateExtensions.setMinute(date9Feb2015, 58);
        assertEquals(58, DateExtensions.getMinute(newDate));
    }

    @Test
    public void testSetSecond() {
        Date newDate = DateExtensions.setSecond(date9Feb2015, 50);
        assertEquals(50, DateExtensions.getSecond(newDate));
    }

    @Test
    public void testSetMillisecond() {
        Date newDate = DateExtensions.setMillisecond(date9Feb2015, 980);
        assertEquals(980, DateExtensions.getMillisecond(newDate));
    }

    @Test
    public void testGetDay() {
        assertEquals(9, DateExtensions.getDay(date9Feb2015));
    }

    @Test
    public void testGetMonth() {
        assertEquals(Calendar.FEBRUARY, DateExtensions.getMonth(date9Feb2015));
    }

    @Test
    public void testGetYear() {
        assertEquals(2015, DateExtensions.getYear(date9Feb2015));
    }

    @Test
    public void testGetHour() {
        assertEquals(22, DateExtensions.getHour(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetMinute() {
        assertEquals(15, DateExtensions.getMinute(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetSecond() {
        assertEquals(13, DateExtensions.getSecond(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testGetMillisecond() {
        assertEquals(789, DateExtensions.getMillisecond(date4March2014H22M15S13Ms789));
    }

    @Test
    public void testToMonthYearString() {
        assertEquals("Feb 2015", DateExtensions.toMonthYearString(date9Feb2015));
    }

    @Test
    public void testToMonthNameDayYearString() {
        assertEquals("Feb 09 2015", DateExtensions.toMonthNameDayYearString(date9Feb2015));
    }

    @Test
    public void testToDateIgnoringTime() {
        Date ignoringTime = DateExtensions.toDateIgnoringTime(date4March2014H22M15S13Ms789);
        assertEquals(2014, DateExtensions.getYear(ignoringTime));
        assertEquals(Calendar.MARCH, DateExtensions.getMonth(ignoringTime));
        assertEquals(4, DateExtensions.getDay(ignoringTime));

        assertEquals(0, DateExtensions.getHour(ignoringTime));
        assertEquals(0, DateExtensions.getMinute(ignoringTime));
        assertEquals(0, DateExtensions.getSecond(ignoringTime));
        assertEquals(0, DateExtensions.getMillisecond(ignoringTime));
    }

    @Test
    public void testToMonthYear() {
        Date ignoringDay = DateExtensions.toMonthYear(date4March2014H22M15S13Ms789);
        assertEquals(1, DateExtensions.getDay(ignoringDay));
        assertEquals(0, DateExtensions.getHour(ignoringDay));
    }

    @Test
    public void testDifferenceInMonths() {
        int difference1 = DateExtensions.differenceInMonths(date9Feb2015, date4March2014H22M15S13Ms789);
        assertEquals(11, difference1);

        int difference2 = DateExtensions.differenceInMonths(date4March2014H22M15S13Ms789, date9Feb2015);
        assertEquals(-11, difference2);
    }
}