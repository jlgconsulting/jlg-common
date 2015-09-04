var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("confirmPopup", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/confirmPopup.html"),
        scope: {
            classWrapper: "=",
            classContent: "=",
            yesText: "=",
            noText: "=",
            messageText: "="
        },
        controller: ["$scope", "globalSharedService",
            function ($scope, globalSharedService) {

            $scope.globalSharedData = globalSharedService.sharedData;
                        
            $scope.confirm = function () {
                
                $scope.globalSharedData.confirmPopup.isConfirmed = true;
                $scope.globalSharedData.confirmPopup.isOpen = false;
            };
            
            $scope.notConfirm = function () {
                
                $scope.globalSharedData.confirmPopup.isConfirmed = false;
                $scope.globalSharedData.confirmPopup.isOpen = false;
            };

            $scope.$watch("yesText", function (newValue, oldValue) {
                if (!newValue) {
                    $scope.yesText = "Yes";
                }
            })

            $scope.$watch("noText", function (newValue, oldValue) {
                if (!newValue) {
                    $scope.yesText = "No";
                }
            })

        }]
    };
});

