var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("itemsWithForwardBack", function () {
    return {
        restrict: "E",
        templateUrl: asdk.getAppPath("app/jlg.common/directives/itemsWithForwardBack.html"),
        scope: {
            selectCurrentItem: "=",
            items: "=",
            displayProperty: "@"
        },
        controller: ["$scope",
        function ($scope) {
            $scope.MAX_HISTORY_STEP_DISPLAY_LENGTH = 40;

            $scope.currentStartIndex = 0;

            if(!$scope.displayProperty){
                $scope.displayProperty="name";
            }

            $scope.showPrevious = function () {
                $scope.currentStartIndex--;
            }

            $scope.showNext = function () {
                $scope.currentStartIndex++;
            }

            $scope.$watch("currentStartIndex", function (newValue, oldValue) {
                if (newValue != null
                    && $scope.items
                    && $scope.items.length>0) {
                    for (var i = 0; i < $scope.items.length; i++) {
                        $scope.items[i].hide = false;
                    }

                    for (var i = 0; i < newValue; i++) {
                        $scope.items[i].hide = true;
                    }
                }                
            });
        }]
    };
});

