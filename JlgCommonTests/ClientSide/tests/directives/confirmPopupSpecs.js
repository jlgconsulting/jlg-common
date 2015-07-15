'use strict';
//It is shared throughout the entire application and currently used when deleting something
describe('jlg.common/confirmPopup tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope,
        globalSharedSrv4Test;
      
    beforeEach(function () {
                       
        module("jlg.common");
        module("alltemplates");
        
        inject(["$rootScope", "$compile", "globalSharedSrv", function ($rootScope, $compile, globalSharedSrv) {
            scope = $rootScope.$new();
            scope.globalSharedData = globalSharedSrv.sharedData;
            globalSharedSrv4Test = globalSharedSrv;            
            scope.globalSharedData.confirmPopup.isOpen = true;
            
            directiveElement = angular.element(
                "<confirm-popup ng-show='globalSharedData.confirmPopup.isOpen'"+
                       "custom-class='globalSharedData.confirmPopup.className'"+
                       "text='globalSharedData.confirmPopup.text'></confirm-popup>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);

    });

    it("use example", function () {
        scope.globalSharedData.confirmPopup.originUniqueToken = "token3";
        scope.globalSharedData.confirmPopup.objectForConfirmation = {
            id: 7
        };
        scope.globalSharedData.confirmPopup.isOpen = true;
        scope.$digest();

        directiveIsolatedScope.confirm();
        expect(scope.globalSharedData.confirmPopup.isConfirmed).toBe(true);
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(false);
        
        //this should be placed inside a $scope.$watch("globalSharedData.confirmPopup.isConfirmed"
        if (scope.globalSharedData.confirmPopup.isConfirmed
            && scope.globalSharedData.confirmPopup.originUniqueToken == "token3") {

            expect(scope.globalSharedData.confirmPopup.objectForConfirmation).toEqual({
                id: 7
            });

            //actions to do if confirmed...

            globalSharedSrv4Test.resetConfirmPopup();

            expect(scope.globalSharedData.confirmPopup.originUniqueToken).toBe(null);
            expect(scope.globalSharedData.confirmPopup.objectForConfirmation).toBe(null);
        }

    });
    
    it("text", function () {
        scope.globalSharedData.confirmPopup.text = "some text";
        scope.$digest();
        expect(directiveIsolatedScope.text).toBe("some text");
    });

    it("customClass", function () {
        scope.globalSharedData.confirmPopup.className = "clasa1";
        scope.$digest();
        expect(directiveIsolatedScope.customClass).toBe("clasa1");
    });
    
    it("confirm", function () {
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(true);
        directiveIsolatedScope.confirm();
        expect(scope.globalSharedData.confirmPopup.isConfirmed).toBe(true);
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(false);
    });
    
    it("notConfirm", function () {
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(true);
        directiveIsolatedScope.notConfirm();
        expect(scope.globalSharedData.confirmPopup.isConfirmed).toBe(false);
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(false);
    });
});