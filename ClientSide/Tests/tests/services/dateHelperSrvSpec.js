"use strict";
describe("jlg.common/dateHelperSrv tests", function () {
    var dateHelperSrv4Test;

    //excuted before each "it" is run.
    beforeEach(function () {

        //load the module.
        module("jlg.common");
        
        //inject your dateHelperSrv for testing.
        inject(["dateHelperSrv", function (dateHelperSrv) {
            dateHelperSrv4Test = dateHelperSrv;
        }]);
    });

    it("addMonths: should work for adding", function () {
        expect(dateHelperSrv4Test.addMonths("Feb 2014", 2)).toBe("Apr 2014");
        expect(dateHelperSrv4Test.addMonths("Dec 2014", 1)).toBe("Jan 2015");
    });
    
    it("addMonths: should work for substracting", function () {
        expect(dateHelperSrv4Test.addMonths("Jun 2014", -3)).toBe("Mar 2014");
        expect(dateHelperSrv4Test.addMonths("Feb 2015", -4)).toBe("Oct 2014");
    });
    
    it("compareMMMyyyyDates: should work for date strings", function () {
        
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Jun 2014", "Jan 2014"))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Aug 2014", "Feb 2012"))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Mar 2014", "Sep 2014"))
            .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Dec 2013", "Jul 2014"))
           .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Oct 2014", "Oct 2014"))
           .toBe(0);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Dec 2010", "Dec 2010"))
          .toBe(0);
    });

    it("compareMMMyyyyDates: should work for date objects", function () {

        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Jun 2014"), new Date("Jan 2014")))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Aug 2014"), new Date("Feb 2012")))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Mar 2014"), new Date("Sep 2014")))
            .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Dec 2013"), new Date("Jul 2014")))
           .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Oct 2014"), new Date("Oct 2014")))
          .toBe(0);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Dec 2010"), new Date("Dec 2010")))
          .toBe(0);
    });
    
    it("compareMMMyyyyDates: should work for mixed string and date objects", function () {

        expect(dateHelperSrv4Test.compareMMMyyyyDates("Jun 2014", new Date("Jan 2014")))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Aug 2014"), "Feb 2012"))
            .toBe(1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Mar 2014", new Date("Sep 2014")))
            .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Dec 2013"), "Jul 2014"))
           .toBe(-1);
        expect(dateHelperSrv4Test.compareMMMyyyyDates("Oct 2014", new Date("Oct 2014")))
          .toBe(0);
        expect(dateHelperSrv4Test.compareMMMyyyyDates(new Date("Dec 2010"), "Dec 2010"))
          .toBe(0);
    });

    it("differenceInMonths: should work", function () {
        expect(dateHelperSrv4Test.differenceInMonths("Jun 2014", "Mar 2014")).toBe(3);
        expect(dateHelperSrv4Test.differenceInMonths("Mar 2014", "Jun 2014")).toBe(3);
        expect(dateHelperSrv4Test.differenceInMonths("Feb 2015", "Oct 2014")).toBe(4);
        expect(dateHelperSrv4Test.differenceInMonths("Oct 2014", "Feb 2015")).toBe(4);
        expect(dateHelperSrv4Test.differenceInMonths("Dec 2014", "Dec 2014")).toBe(0);
    });
    
    it("getDatesForNextMonths: should work", function () {
        expect(dateHelperSrv4Test.getDatesForNextMonths("Jun 2014", 4))
            .toEqual(["Jul 2014", "Aug 2014", "Sep 2014", "Oct 2014"]);
        
        expect(dateHelperSrv4Test.getDatesForNextMonths("Nov 2014", 3))
            .toEqual(["Dec 2014", "Jan 2015", "Feb 2015"]);
        
        expect(dateHelperSrv4Test.getDatesForNextMonths("Sep 2014", -2))
            .toEqual(["Jul 2014", "Aug 2014"]);
        
    });
    
    it("getSelectedDateRangeFromEnd: should work", function () {
        var allDates = ["Sep 2014", "Oct 2014", "Nov 2014", "Dec 2014", "Jan 2015", "Feb 2015", "Mar 2015"];

        expect(dateHelperSrv4Test.getSelectedDateRangeFromEnd(allDates, -2))
            .toBe(null);
        
        expect(dateHelperSrv4Test.getSelectedDateRangeFromEnd(allDates, 4))
            .toEqual({
                from: {
                    index: 2,
                    date: "Nov 2014"
                },
                to: {
                    index: 6,
                    date: "Mar 2015"
                }
            });

        expect(dateHelperSrv4Test.getSelectedDateRangeFromEnd(allDates, 2))
             .toEqual({
                 from: {
                     index: 4,
                     date: "Jan 2015"
                 },
                 to: {
                     index: 6,
                     date: "Mar 2015"
                 }
             });

    });
    
    it("getYearMonth: should work", function () {
        
        expect(dateHelperSrv4Test.getYearMonth("Nov 2014"))
            .toEqual({
                month: 11,
                monthName: "Nov",
                year: 2014
            });
        
        expect(dateHelperSrv4Test.getYearMonth("Jul 2015"))
            .toEqual({
                month: 7,
                monthName: "Jul",
                year: 2015
            });
        
        expect(dateHelperSrv4Test.getYearMonth("Feb 2016"))
            .toEqual({
                month: 2,
                monthName: "Feb",
                year: 2016
            });
    });

    it("isDateObject: should work", function () {

        expect(dateHelperSrv4Test.isDateObject("Nov 2014"))
            .toBe(false);

        expect(dateHelperSrv4Test.isDateObject(new Date("Jul 2015")))
            .toBe(true);
    });
    
    it("toMMMyyyyDateString: should work", function () {

        expect(dateHelperSrv4Test.toMMMyyyyDateString(new Date("Nov 2014")))
            .toBe("Nov 2014");

        expect(dateHelperSrv4Test.toMMMyyyyDateString(new Date("Jul 2015")))
            .toBe("Jul 2015");
    });
    
    it("toMMMyyyyDateStringFrom: should work", function () {

        expect(dateHelperSrv4Test.toMMMyyyyDateStringFrom(2, 2014))
            .toBe("Feb 2014");

        expect(dateHelperSrv4Test.toMMMyyyyDateStringFrom(7, 2015))
            .toBe("Jul 2015");
    });

    it("getMonthsNrBetween: should work", function () {

        expect(dateHelperSrv4Test.getMonthsNrBetween("Nov 2014", "Dec 2014"))
            .toBe(1);
        expect(dateHelperSrv4Test.getMonthsNrBetween("Dec 2014", "Nov 2014"))
            .toBe(1);

        expect(dateHelperSrv4Test.getMonthsNrBetween("Nov 2014", "Mar 2015"))
            .toBe(4);
        expect(dateHelperSrv4Test.getMonthsNrBetween("Mar 2015", "Dec 2014"))
           .toBe(3);
    });

});
