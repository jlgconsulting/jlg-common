'use strict';
//It is shared throughout the entire application and currently used when deleting something
describe('jlg.common/confirmPopup tests:', function () {
    var scope,
        directiveElement,
        directiveIsolatedScope,
        globalSharedService4Test;
	
	var wasCalledConfirmCallBack = false;
	var wasCalledNotConfirmCallBack = false;
      
    beforeEach(function () {
                       
        module("jlg.common");
        module("alltemplates");
        
        inject(["$rootScope", "$compile", "globalSharedService", function ($rootScope, $compile, globalSharedService) {
            scope = $rootScope.$new();
            scope.globalSharedData = globalSharedService.sharedData;
            globalSharedService4Test = globalSharedService;            
            scope.globalSharedData.confirmPopup.isOpen = true;
            scope.globalSharedData.confirmPopup.confirmCallBack = function(){
				wasCalledConfirmCallBack = true;
			};

			scope.globalSharedData.confirmPopup.notConfirmCallBack = function(){
				wasCalledNotConfirmCallBack = true;
			};			
			
			
            directiveElement = angular.element(
                "<confirm-popup"+
                       " options='globalSharedData.confirmPopup'" +
                       " class-wrapper='globalSharedData.confirmPopup.classWrapper'></confirm-popup>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);

    });
    
    
        
    it("confirm", function () {
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(true);
		expect(wasCalledConfirmCallBack).toBe(false);
        directiveIsolatedScope.confirm();
		expect(wasCalledConfirmCallBack).toBe(true);
        expect(scope.globalSharedData.confirmPopup.isConfirmed).toBe(true);
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(false);
    });
    
    it("notConfirm", function () {
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(true);
		expect(wasCalledNotConfirmCallBack).toBe(false);
        directiveIsolatedScope.notConfirm();
		expect(wasCalledNotConfirmCallBack).toBe(true);
        expect(scope.globalSharedData.confirmPopup.isConfirmed).toBe(false);
        expect(scope.globalSharedData.confirmPopup.isOpen).toBe(false);
    });
});