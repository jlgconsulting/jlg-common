package extensions;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

/**
 * Created by Dan on 04/09/2015.
 */
public final class DateTimeExtensions {

    public static Date createDate(int year, int month, int day){
        Calendar cal = Calendar.getInstance();
        cal.set(Calendar.YEAR,year);
        cal.set(Calendar.MONTH, month);
        cal.set(Calendar.DAY_OF_MONTH,day);
        return cal.getTime();
    }

    public static Date addDays(Date date, int days){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.DATE, days);
        return cal.getTime();
    }

    public static Date addMonths(Date date, int months){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.MONTH, months);
        return cal.getTime();
    }

    public static Date addYears(Date date, int years){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.YEAR, years);
        return cal.getTime();
    }

    public static Date setDay(Date date, int day){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.set(Calendar.DAY_OF_MONTH, day);
        return cal.getTime();
    }

    public static Date setMonth(Date date, int month){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.set(Calendar.MONTH, month);
        return cal.getTime();
    }

    public static Date setYear(Date date, int year){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.set(Calendar.YEAR, year);
        return cal.getTime();
    }

    public static int getDay(Date date){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        int day = cal.get(Calendar.DAY_OF_MONTH);
        return day;
    }

    public static int getMonth(Date date){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        int month = cal.get(Calendar.MONTH);
        return month;
    }

    public static int getYear(Date date){
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        int year = cal.get(Calendar.YEAR);
        return year;
    }

    public static String toMonthYearString(Date date)
    {
        SimpleDateFormat dateFormat = new SimpleDateFormat("MMM yyyy", Locale.US);
        return dateFormat.format(date);
    }

    public static String toMonthNameDayYearString(Date date)
    {
        SimpleDateFormat dateFormat = new SimpleDateFormat("MMM dd yyyy", Locale.US);
        return dateFormat.format(date);
    }

    public static Date toDateIgnoringTime(Date date)
    {
        Calendar calendar= Calendar.getInstance();
        calendar.setTime(date);
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.HOUR, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND, 0);
        date=calendar.getTime();
        return date;
    }

    public static Date toMonthYear(Date date)
    {
        date = toDateIgnoringTime(date);
        Calendar calendar= Calendar.getInstance();
        calendar.setTime(date);
        calendar.set(Calendar.DAY_OF_MONTH, 1);
        date=calendar.getTime();
        return date;
    }

    public static int differenceInMonths(Date currentDate, Date otherDate)
    {
        currentDate = toMonthYear(currentDate);
        otherDate = toMonthYear(otherDate);

        boolean otherIsGreater = false;
        if (currentDate.before(otherDate))
        {
            otherIsGreater = true;
        }

        Date start;
        Date end;

        if (otherIsGreater)
        {
            start = currentDate;
            end = otherDate;
        }
        else
        {
            start = otherDate;
            end = currentDate;
        }

        int difference = 0;
        while (start.before(end))
        {
            start = addMonths(start, 1);
            difference++;
        }

        if (otherIsGreater)
        {
            return -difference;
        }
        else
        {
            return difference;
        }
    }

    private DateTimeExtensions(){

    }
}
