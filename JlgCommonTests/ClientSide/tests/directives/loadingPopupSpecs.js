'use strict';
describe('jlg.common.directives/loadingPopup tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;
    
    beforeEach(function () {

        module("alltemplates");
        module("jlg.common.directives");

        inject(["$rootScope", "$compile", function($rootScope, $compile) {
            scope = $rootScope.$new();
            directiveElement = angular.element(
                "<loading-popup custom-class='className'"+
                               "text='customText'></loading-popup>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });
    
    it("customClass", function () {
        scope.className = "custom-class-name";
        scope.$digest();

        expect(directiveIsolatedScope.customClass).toBe("custom-class-name");
    });

    it("text", function () {
        scope.customText = "Loading...";
        scope.$digest();

        expect(directiveIsolatedScope.text).toBe("Loading...");
    });
});