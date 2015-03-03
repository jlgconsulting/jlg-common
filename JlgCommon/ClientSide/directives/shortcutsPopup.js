var directivesModule = angular.module("directives");
directivesModule.directive("shortcutsPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/shortcutsPopup.html"),
        scope: {
            
        },
        controller: ["$scope", "apfSharedDataAndPopupSrv",
            function ($scope, apfSharedDataAndPopupSrv) {

            $scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;
                }
            });

            $scope.goBack = function() {
                $scope.apfSharedData.shortcutsPopup.isOpen = false;
            };
        }]
    };
});

