'use strict';
describe('jlg.common/sectionTitle tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    beforeEach(function () {     
        module("jlg.common");
        module("alltemplates");
        
        inject(["$rootScope", "$compile", "globalSharedSrv", function ($rootScope, $compile, globalSharedSrv) {
            scope = $rootScope.$new();
            scope.globalSharedData = globalSharedSrv.sharedData;
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

        scope.globalSharedData.sectionTitle.show = true;
        scope.globalSharedData.sectionTitle.title = "Custom title";
        scope.globalSharedData.sectionTitle.goBackCallback = goBack;
        scope.$digest();
        expect(directiveIsolatedScope.globalSharedData.sectionTitle.show).toBe(true);
        expect(directiveIsolatedScope.globalSharedData.sectionTitle.title).toBe("Custom title");
        expect(wentBack).toBe(false);
        directiveIsolatedScope.globalSharedData.sectionTitle.goBackCallback();
        expect(wentBack).toBe(true);
    });
});