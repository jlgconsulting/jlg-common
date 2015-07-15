var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("loadingPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/directives/loadingPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        }
    };
});

