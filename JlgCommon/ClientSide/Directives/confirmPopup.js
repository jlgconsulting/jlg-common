var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("confirmPopup", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/Directives/confirmPopup.html"),
        scope: {
            customClass: "=",
            text: "="
        },
        controller: ["$scope", "sharedDataAndPopupSrv",
            function ($scope, sharedDataAndPopupSrv) {

            $scope.apfSharedData = sharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
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
                
                $scope.apfSharedData.confirmPopup.isConfirmed = true;
                $scope.apfSharedData.confirmPopup.isOpen = false;
            };
            
            $scope.notConfirm = function () {
                
                $scope.apfSharedData.confirmPopup.isConfirmed = false;
                $scope.apfSharedData.confirmPopup.isOpen = false;
            };

        }]
    };
});

