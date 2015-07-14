'use strict';
describe('jlg.common.directives/comparingArrow tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope;
      
    beforeEach(function () {

        module("alltemplates");
        module("jlg.common.directives");

        inject(["$rootScope", "$compile", function($rootScope, $compile) {
            scope = $rootScope.$new();
            directiveElement = angular.element(
                "<comparing-arrow nr1='number1' nr2='number2'></comparing-arrow>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);

    });

    it("initialization", function () {
        expect(directiveIsolatedScope.showRight).toBe(false);
        expect(directiveIsolatedScope.showDown).toBe(false);
        expect(directiveIsolatedScope.showUp).toBe(false);
    });
        
    it("showDown", function () {
        scope.number1 = 7;
        scope.number2 = 13;
        scope.$digest();

        expect(directiveIsolatedScope.showRight).toBe(false);
        expect(directiveIsolatedScope.showDown).toBe(true);
        expect(directiveIsolatedScope.showUp).toBe(false);
    });

    it("showUp", function () {
        scope.number1 = 10;
        scope.number2 = 3;
        scope.$digest();

        expect(directiveIsolatedScope.showRight).toBe(false);
        expect(directiveIsolatedScope.showDown).toBe(false);
        expect(directiveIsolatedScope.showUp).toBe(true);
    });

    it("showRight", function () {
        scope.number1 = 7;
        scope.number2 = 7;
        scope.$digest();

        expect(directiveIsolatedScope.showRight).toBe(true);
        expect(directiveIsolatedScope.showDown).toBe(false);
        expect(directiveIsolatedScope.showUp).toBe(false);
    });
});