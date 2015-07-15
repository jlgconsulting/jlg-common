var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("loadingPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/Directives/loadingPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        }
    };
});

