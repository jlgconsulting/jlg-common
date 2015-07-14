var directivesModule = angular.module("jlg.common.directives");
directivesModule.directive("loadingPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/Directives/loadingPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        }
    };
});

