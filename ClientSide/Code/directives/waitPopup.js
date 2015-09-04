var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("waitPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/waitPopup.html"),
        scope: {
            classWrapper: "@",
            classContent: "@",
            messageText: "@"
        }
    };
});

