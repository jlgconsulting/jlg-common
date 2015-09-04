var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("treeDm", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/treeDm.html"),
        scope: {
            nameProperty: "@",
            childrenProperty: "@",
            rootNodesArray: "=",
            selectedNode: "="
        },
        controller: ["$scope",
            function ($scope) {
           
            $scope.setSelectedNode = function (node) {
                $scope.selectedNode = node;
            };
            
        }]
    };
});