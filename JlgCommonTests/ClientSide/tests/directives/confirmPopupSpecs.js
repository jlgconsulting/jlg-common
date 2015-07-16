'use strict';
//It is shared throughout the entire application and currently used when deleting something
describe('jlg.common/confirmPopup tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope,
        globalSharedService4Test;
      
    beforeEach(function () {
                       
        module("jlg.common");
        module("alltemplates");
        
        inject(["$rootScope", "$compile", "globalSharedService", function ($rootScope, $compile, globalSharedService) {
            scope = $rootScope.$new();
            scope.globalSharedData = globalSharedService.sharedData;
            globalSharedService4Test = globalSharedService;            
            scope.globalSharedData.confirmPopup.isOpen = true;
            
            directiveElement = angular.element(
                "<confirm-popup"+
                       " class-wrapper='globalSharedData.confirmPopup.classWrapper'" +
                       " class-content='globalSharedData.confirmPopup.classContent'" +
                       " yes-text='globalSharedData.confirmPopup.yesText'" +
                       " no-text='globalSharedData.confirmPopup.noText'" +
                       " message-text='globalSharedData.confirmPopup.messageText'></confirm-popup>");
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

            globalSharedService4Test.resetConfirmPopup();

            expect(scope.globalSharedData.confirmPopup.originUniqueToken).toBe(null);
            expect(scope.globalSharedData.confirmPopup.objectForConfirmation).toBe(null);
        }

    });
    
    it("text", function () {
        scope.globalSharedData.confirmPopup.messageText = "some text";
        scope.$digest();
        expect(directiveIsolatedScope.messageText).toBe("some text");
    });

    it("classWrapper", function () {
        scope.globalSharedData.confirmPopup.classWrapper = "class1";
        scope.$digest();
        expect(directiveIsolatedScope.classWrapper).toBe("class1");
    });

    it("classContent", function () {
        scope.globalSharedData.confirmPopup.classContent = "class2";
        scope.$digest();
        expect(directiveIsolatedScope.classContent).toBe("class2");
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