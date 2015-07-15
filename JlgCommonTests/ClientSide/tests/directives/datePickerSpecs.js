'use strict';
describe('jlg.common/datePicker tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope;
    
    beforeEach(function () {       
        module("jlg.common");
        module("alltemplates");
        module("jlg.common");

        inject(["$rootScope", "$compile", function ($rootScope, $compile) {
            scope = $rootScope.$new();
            scope.selectedDate = null;
            scope.selectedYear = null;
            scope.selectedMonth = null;
            directiveElement = angular.element(
                 "<date-picker disabled='isDisabled'"+
                              "selected='selectedDate'" +
                              "selected-year='selectedYear'" +
                              "selected-month='selectedMonth'" +
                              "min='minDate'"+
                              "max='maxDate'></date-picker>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });

    it("bindings", function () {
        scope.isDisabled = false;
        scope.minDate = "Jan 2010";
        scope.maxDate = "Aug 2013";
        scope.$digest();
        expect(directiveIsolatedScope.disabled).toBe(false);
        expect(directiveIsolatedScope.min).toBe("Jan 2010");
        expect(directiveIsolatedScope.max).toBe("Aug 2013");
        expect(directiveIsolatedScope.selected).toBe(null);
    });

    it("select", function() {
        directiveIsolatedScope.selected = "Feb 2012";
        directiveIsolatedScope.$digest();
        expect(directiveIsolatedScope.selectedLocal).toBe("Feb 2012");
        expect(directiveIsolatedScope.selectedYear).toBe(2012);
        expect(directiveIsolatedScope.selectedMonth).toBe(2);
    });
});