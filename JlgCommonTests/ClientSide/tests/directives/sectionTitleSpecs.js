'use strict';
describe('directives/sectionTitle tests:', function () {
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
            directiveElement = angular.element(
                "<section-title></section-title>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });

    it("initialization", function () {
        var wentBack = false;
        var goBack = function() {
            wentBack = true;
        };

        scope.apfSharedData.sectionTitle.show = true;
        scope.apfSharedData.sectionTitle.title = "Custom title";
        scope.apfSharedData.sectionTitle.goBackCallback = goBack;
        scope.$digest();
        expect(directiveIsolatedScope.apfSharedData.sectionTitle.show).toBe(true);
        expect(directiveIsolatedScope.apfSharedData.sectionTitle.title).toBe("Custom title");
        expect(wentBack).toBe(false);
        directiveIsolatedScope.apfSharedData.sectionTitle.goBackCallback();
        expect(wentBack).toBe(true);
    });
});