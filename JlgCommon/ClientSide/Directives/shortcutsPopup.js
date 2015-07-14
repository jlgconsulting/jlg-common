var directivesModule = angular.module("jlg.common.directives");
directivesModule.directive("shortcutsPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/Directives/shortcutsPopup.html"),
        scope: {
            
        },
        controller: ["$scope", "sharedDataAndPopupSrv",
            function ($scope, sharedDataAndPopupSrv) {

            $scope.apfSharedData = sharedDataAndPopupSrv.sharedData;
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

