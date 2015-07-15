'use strict';
//It is shared throughout the entire application and currently used when deleting something
describe('jlg.common/confirmPopup tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope,
        sharedDataAndPopupSrv4Test;
      
    beforeEach(function () {
        
        module("jlg.common");        
        module("jlg.common");
        module("alltemplates");
        
        inject(["$rootScope", "$compile", "sharedDataAndPopupSrv", function ($rootScope, $compile, sharedDataAndPopupSrv) {
            scope = $rootScope.$new();
            scope.apfSharedData = sharedDataAndPopupSrv.sharedData;
            sharedDataAndPopupSrv4Test = sharedDataAndPopupSrv;            
            scope.apfSharedData.confirmPopup.isOpen = true;
            
            directiveElement = angular.element(
                "<confirm-popup ng-show='apfSharedData.confirmPopup.isOpen'"+
                       "custom-class='apfSharedData.confirmPopup.className'"+
                       "text='apfSharedData.confirmPopup.text'></confirm-popup>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);

    });

    it("use example", function () {
        scope.apfSharedData.confirmPopup.originUniqueToken = "token3";
        scope.apfSharedData.confirmPopup.objectForConfirmation = {
            id: 7
        };
        scope.apfSharedData.confirmPopup.isOpen = true;
        scope.$digest();

        directiveIsolatedScope.confirm();
        expect(scope.apfSharedData.confirmPopup.isConfirmed).toBe(true);
        expect(scope.apfSharedData.confirmPopup.isOpen).toBe(false);
        
        //this should be placed inside a $scope.$watch("apfSharedData.confirmPopup.isConfirmed"
        if (scope.apfSharedData.confirmPopup.isConfirmed
            && scope.apfSharedData.confirmPopup.originUniqueToken == "token3") {

            expect(scope.apfSharedData.confirmPopup.objectForConfirmation).toEqual({
                id: 7
            });

            //actions to do if confirmed...

            sharedDataAndPopupSrv4Test.resetConfirmPopup();

            expect(scope.apfSharedData.confirmPopup.originUniqueToken).toBe(null);
            expect(scope.apfSharedData.confirmPopup.objectForConfirmation).toBe(null);
        }

    });
    
    it("text", function () {
        scope.apfSharedData.confirmPopup.text = "some text";
        scope.$digest();
        expect(directiveIsolatedScope.text).toBe("some text");
    });

    it("customClass", function () {
        scope.apfSharedData.confirmPopup.className = "clasa1";
        scope.$digest();
        expect(directiveIsolatedScope.customClass).toBe("clasa1");
    });
    
    it("confirm", function () {
        expect(scope.apfSharedData.confirmPopup.isOpen).toBe(true);
        directiveIsolatedScope.confirm();
        expect(scope.apfSharedData.confirmPopup.isConfirmed).toBe(true);
        expect(scope.apfSharedData.confirmPopup.isOpen).toBe(false);
    });
    
    it("notConfirm", function () {
        expect(scope.apfSharedData.confirmPopup.isOpen).toBe(true);
        directiveIsolatedScope.notConfirm();
        expect(scope.apfSharedData.confirmPopup.isConfirmed).toBe(false);
        expect(scope.apfSharedData.confirmPopup.isOpen).toBe(false);
    });
});