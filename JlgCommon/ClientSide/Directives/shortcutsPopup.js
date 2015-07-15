var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("shortcutsPopup", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/directives/shortcutsPopup.html"),
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

