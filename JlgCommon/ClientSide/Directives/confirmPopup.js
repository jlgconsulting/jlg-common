var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("confirmPopup", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/directives/confirmPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        },
        controller: ["$scope", "globalSharedSrv",
            function ($scope, globalSharedSrv) {

            $scope.globalSharedData = globalSharedSrv.sharedData;
            $scope.$watch("globalSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;
                    if (!$scope.text) {
                        $scope.text = $scope.translatedText.pleaseConfirm;
                    }
                }
            });

            $scope.$watch("text", function (newValue) {
                if (!newValue 
                    && $scope.translatedText) {
                    
                    $scope.text = $scope.translatedText.pleaseConfirm;
                }
            });

            $scope.confirm = function () {
                
                $scope.globalSharedData.confirmPopup.isConfirmed = true;
                $scope.globalSharedData.confirmPopup.isOpen = false;
            };
            
            $scope.notConfirm = function () {
                
                $scope.globalSharedData.confirmPopup.isConfirmed = false;
                $scope.globalSharedData.confirmPopup.isOpen = false;
            };

        }]
    };
});

