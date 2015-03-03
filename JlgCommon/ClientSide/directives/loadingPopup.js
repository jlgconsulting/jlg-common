var directivesModule = angular.module("directives");
directivesModule.directive("loadingPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/loadingPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        }
    };
});

