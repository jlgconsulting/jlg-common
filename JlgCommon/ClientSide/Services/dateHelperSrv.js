var sharedModule = angular.module("jlg.common.services");
sharedModule.factory('dateHelperSrv',
    [
    function () {
        
    var months = ["-","Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var monthsByName = {};
    monthsByName["Jan"] = 1;
    monthsByName["Feb"] = 2;
    monthsByName["Mar"] = 3;
    monthsByName["Apr"] = 4;
    monthsByName["May"] = 5;
    monthsByName["Jun"] = 6;
    monthsByName["Jul"] = 7;
    monthsByName["Aug"] = 8;
    monthsByName["Sep"] = 9;
    monthsByName["Oct"] = 10;
    monthsByName["Nov"] = 11;
    monthsByName["Dec"] = 12;
  
    var toMMMyyyyDateString = function (dateObjOrStr) {
        if (isDateObject(dateObjOrStr)) {
            return months[dateObjOrStr.getMonth() + 1] + " " + dateObjOrStr.getFullYear();
        } else {
            return dateObjOrStr;
        }
    };
        
    var toMMMyyyyDateStringFrom = function (month, year) {
        return months[month] + " " + year;
    };

    var isDateObject = function (date) {
        if (date.getMonth && date.getFullYear) {
            return true;
        }

        return false;
    };

    var compareMMMyyyyDates = function(date1, date2) {
        /* 1 - date1 > date2
           -1 - date1 < date2
           0 - date1 = date2        
        */

        var date1Str;
        var date2Str;

        if (isDateObject(date1)) {
            date1Str = toMMMyyyyDateString(date1);
        } else {
            date1Str = date1;
        }

        if (isDateObject(date2)) {
            date2Str = toMMMyyyyDateString(date2);
        } else {
            date2Str = date2;
        }

        var date1Components = date1Str.split(" ");
        date1Month = date1Components[0];
        date1Year = parseInt(date1Components[1]);

        var date2Components = date2Str.split(" ");
        date2Month = date2Components[0];
        date2Year = parseInt(date2Components[1]);

        if (date1Year > date2Year) {
            return 1;
        }

        if (date1Year < date2Year) {
            return -1;
        }

        if (date1Year == date2Year) {
            var date1MonthIndex = months.indexOf(date1Month);
            var date2MonthIndex = months.indexOf(date2Month);

            if (date1MonthIndex > date2MonthIndex) {
                return 1;
            }

            if (date1MonthIndex < date2MonthIndex) {
                return -1;
            }

            if (date1MonthIndex == date2MonthIndex) {
                return 0;
            }
        }
    };

    var getYearMonth = function (dateString) {
        var monthName = dateString.substring(0, 3);
        return {
            month: monthsByName[monthName],
            monthName: monthName,
            year: parseInt(dateString.substring(4, 8))
        };
    };

    var differenceInMonths = function(dateString1, dateString2) {
        if (compareMMMyyyyDates(dateString1, dateString2) === 0) {
            return 0;
        }
        
        var dateGreater = null;
        var dateSmaller = null;
        if (compareMMMyyyyDates(dateString1, dateString2) === 1) {
            dateGreater = getYearMonth(dateString1);
            dateSmaller = getYearMonth(dateString2);
        } else {
            dateGreater = getYearMonth(dateString2);
            dateSmaller = getYearMonth(dateString1);
        }

        if (dateGreater.year == dateSmaller.year) {
            return dateGreater.month - dateSmaller.month;
        } else {
            var years = dateGreater.year - dateSmaller.year;
            if (dateGreater.month >= dateSmaller.month) {
                return 12 * years + (dateGreater.month - dateSmaller.month);
            } else {
                return 12 * years - (dateSmaller.month - dateGreater.month);
            }
        }
    };

    var getSelectedDateRangeFromEnd = function (allDates, monthsRange) {
        if (monthsRange < 0) {
            return null;
        }

        var selectedDateRange = {};

        selectedDateRange.to = {
            index: allDates.length - 1,
            date: allDates[allDates.length - 1]
        };

        if (!monthsRange) {
            selectedDateRange.from = {
                index: 0,
                date: allDates[0]
            };
        } else {
            for (var i = 0; i < allDates.length; i++) {
                if (differenceInMonths(allDates[allDates.length - 1], allDates[i]) <= monthsRange) {
                    selectedDateRange.from = {
                        index: i,
                        date: allDates[i]
                    };
                    break;
                }
            }
        }
        
        return selectedDateRange;
    };

    var addMonths = function (dateString, numberOfMonths) {
        if (!dateString) {
            return null;
        }

        var date = getYearMonth(dateString);

        if (numberOfMonths >= 0) {
            for (var i = 1; i <= numberOfMonths; i++) {
                if (date.month <= 11) {
                    date.month++;
                } else {
                    date.month = 1;
                    date.year++;
                }
            }
        } else {
            numberOfMonths = 0 - numberOfMonths;
            for (var i = 1; i <= numberOfMonths; i++) {
                if (date.month > 1) {
                    date.month--;
                } else {
                    date.month = 12;
                    date.year--;
                }
            }
        }
        
        return months[date.month] + " " + date.year;
    };

    var getMonthsNrBetween = function(dateStartString, dateEndString) {

        var startDateStr;
        var endDateStr;

        var compareResult = compareMMMyyyyDates(dateStartString, dateEndString);
        if (compareResult == 0) {
            return 0;
        }else if (compareResult == -1) {
            startDateStr = dateStartString;
            endDateStr = dateEndString;
        } else {
            startDateStr = dateEndString;
            endDateStr = dateStartString;
        }

        var theCurrentDate = startDateStr;
        var monthsNr = 0;
        while (compareMMMyyyyDates(theCurrentDate, endDateStr) == -1) {
            monthsNr++;
            theCurrentDate = addMonths(theCurrentDate, 1);
        }
        return monthsNr;
    };

    var getDatesForNextMonths = function (dateString, numberOfMonths) {
        var nextDates = [];
        var date = getYearMonth(dateString);
        if (numberOfMonths > 0) {
            for (var i = 1; i <= numberOfMonths; i++) {
                nextDates.push(addMonths(dateString, i));
            }
        } else {
            for (var i = 0-numberOfMonths; i>=1; i--) {
                nextDates.push(addMonths(dateString, 0-i));
            }
        }
        return nextDates;
    };

    return {
        addMonths: addMonths,
        compareMMMyyyyDates: compareMMMyyyyDates,
        differenceInMonths: differenceInMonths,
        getDatesForNextMonths: getDatesForNextMonths,
        getSelectedDateRangeFromEnd: getSelectedDateRangeFromEnd,
        getYearMonth: getYearMonth,
        isDateObject: isDateObject,
        toMMMyyyyDateString: toMMMyyyyDateString,
        toMMMyyyyDateStringFrom: toMMMyyyyDateStringFrom,
        getMonthsNrBetween: getMonthsNrBetween
    };
}]);
