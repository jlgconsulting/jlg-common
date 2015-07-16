'use strict';
describe('jlg.common/waitPopup tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;
    
    beforeEach(function () {

        module("alltemplates");
        module("jlg.common");

        inject(["$rootScope", "$compile", function ($rootScope, $compile) {
            scope = $rootScope.$new();

            directiveElement = angular.element(
                 "<wait-popup class-wrapper='classWrapper'" +
                               " class-content='classContent'" +
                               " message-text='messageText'></wait-popup>");
            
            $compile(directiveElement)(scope);
            scope.$digest();            
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });
    
    it("classContent", function () {
        scope.classContent = "custom-class-name-content";
        scope.$digest();

        expect(directiveIsolatedScope.classContent).toBe("custom-class-name-content");
    });

    it("classWrapper", function () {
        scope.classWrapper = "custom-class-name-wrapper";
        scope.$digest();

        expect(directiveIsolatedScope.classWrapper).toBe("custom-class-name-wrapper");
    });


    it("messageText", function () {
        scope.messageText = "Loading...";
        scope.$digest();

        expect(directiveIsolatedScope.messageText).toBe("Loading...");
    });
});