package tests.extensions;

import extensions.DateTimeExtensions;
import org.junit.Before;
import org.junit.Test;

import java.util.Calendar;
import java.util.Date;

import static org.junit.Assert.*;

/**
 * Created by Dan on 04/09/2015.
 */
public class DateTimeExtensionsTest {

    private Date Date2015;
    private Date Date2014;
    @Before
    public void setUp() {
        Date2015 = DateTimeExtensions.createDate(2015, Calendar.FEBRUARY, 9);
        Date2014 = DateTimeExtensions.createDate(2014, Calendar.MARCH, 4);
    }

    @Test
    public void testCreateDate() {
        assertEquals(2015,DateTimeExtensions.getYear(Date2015));
        assertEquals(Calendar.FEBRUARY,DateTimeExtensions.getMonth(Date2015));
        assertEquals(9,DateTimeExtensions.getDay(Date2015));
    }

    @Test
    public void testAddDays() {
        Date2015 = DateTimeExtensions.addDays(Date2015, 1);
        assertEquals(10, DateTimeExtensions.getDay(Date2015));
    }

   /* @Test
    public void testAddMonths() throws Exception {

    }

    @Test
    public void testAddYears() throws Exception {

    }

    @Test
    public void testSetDay() throws Exception {

    }

    @Test
    public void testSetMonth() throws Exception {

    }

    @Test
    public void testSetYear() throws Exception {

    }

    @Test
    public void testGetDay() throws Exception {

    }

    @Test
    public void testGetMonth() throws Exception {

    }

    @Test
    public void testGetYear() throws Exception {

    }

    @Test
    public void testToMonthYearString() throws Exception {

    }

    @Test
    public void testToMonthNameDayYearString() throws Exception {

    }

    @Test
    public void testToDateIgnoringTime() throws Exception {

    }

    @Test
    public void testToMonthYear() throws Exception {

    }

    @Test
    public void testDifferenceInMonths() throws Exception {

    }*/
}