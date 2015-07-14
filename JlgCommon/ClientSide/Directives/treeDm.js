var directivesModule = angular.module("jlg.common.directives");
directivesModule.directive("treeDm", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/Directives/treeDm.html"),
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