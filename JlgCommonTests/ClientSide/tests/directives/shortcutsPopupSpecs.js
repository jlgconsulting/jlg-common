'use strict';
describe('directives/shortcutsPopup tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    beforeEach(function () {
        module("shared");
        module("alltemplates");
        module("directives");

        inject(["$rootScope", "$compile", "apfSharedDataAndPopupSrv", function ($rootScope, $compile, apfSharedDataAndPopupSrv) {
            scope = $rootScope.$new();
            scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            scope.apfSharedData.shortcutsPopup.isOpen = true;
            directiveElement = angular.element(
                "<shortcuts-popup></shortcuts-popup>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });
    
    it("goBack function", function () {
        
        expect(scope.apfSharedData.shortcutsPopup.isOpen).toBe(true);
        directiveIsolatedScope.goBack();
        scope.$digest();
        expect(scope.apfSharedData.shortcutsPopup.isOpen).toBe(false);
    });
});