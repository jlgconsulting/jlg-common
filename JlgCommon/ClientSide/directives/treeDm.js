var directivesModule = angular.module("directives");
directivesModule.directive("treeDm", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/treeDm.html"),
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