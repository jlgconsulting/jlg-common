var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("confirmPopup", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/confirmPopup.html"),
        scope: {
            options: "="
        },
        controller: ["$scope",
            function ($scope) {
                        
            $scope.confirm = function () {
                $scope.options.isConfirmed = true;
                $scope.options.isOpen = false;
                if($scope.options.confirmCallBack){
                    $scope.options.confirmCallBack();
                }
            };

            $scope.notConfirm = function () {
                $scope.options.isConfirmed = false;
                $scope.options.isOpen = false;

                if($scope.options.notConfirmCallBack){
                    $scope.options.notConfirmCallBack();
                }
            };
        }]
    };
});

