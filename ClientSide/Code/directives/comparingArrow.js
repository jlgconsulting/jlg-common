var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("comparingArrow", function () {
    return {        
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/comparingArrow.html"),
        scope: {
            nr1: "=",
            nr2: "="
        },
        controller: ["$scope",
            function ($scope) {
            
            $scope.showRight = false;
            $scope.showDown = false;
            $scope.showUp = false;

            var chooseArrow = function() {
                if ($scope.nr1 == $scope.nr2) {
                    $scope.showRight = true;
                    $scope.showDown = false;
                    $scope.showUp = false;
                } else if ($scope.nr1 < $scope.nr2) {
                    $scope.showRight = false;
                    $scope.showDown = true;
                    $scope.showUp = false;
                } else {
                    $scope.showRight = false;
                    $scope.showDown = false;
                    $scope.showUp = true;
                }
            };

            $scope.$watch("nr1", function (newValue) {
                if (newValue) {
                    chooseArrow();
                }
            });
            
            $scope.$watch("nr2", function (newValue) {
                if (newValue) {
                    chooseArrow();
                }
            });
        }]
    };
});

