var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("gridDm", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/directives/gridDm.html"),
        scope: {
            rows: "=",
            options: "=",
            tableClass: "@"
        },
        controller: ["$scope", "$filter", "$sce", "$compile",
            function ($scope, $filter, $sce, $compile) {

            $scope.$watch("rows", function (newValue, oldValue) {
                if (newValue) {
                    executeCurrentSortAgain();
                }
            }, true);
           
            $scope.getDeepPropertyValueFromStringPath = function (obj, pathToProperty) {
                if (!obj) {
                    return obj;
                }

                var propertiesChainToDestination = pathToProperty.split(".");
                for (var i = 0; i < propertiesChainToDestination.length; i++) {
                    obj = obj[propertiesChainToDestination[i]];
                };
                return obj;
            };

            $scope.toggleSort = function (column) {
                if (typeof column.isAscending === "undefined") {
                    column.isAscending = null;
                }

                resetSortForAllExcepting(column);

                switch (column.isAscending) {
                    case null:
                        column.isAscending = true;
                        break;
                    case true:
                        column.isAscending = false;
                        break;
                    case false:
                        column.isAscending = null;
                        break;
                }

                sort(column);
            };

            $scope.trustAsHtml = function(text) {
               return $sce.trustAsHtml(text+"");
            };

            $scope.getHtml = function (textHtml) {
                return textHtml;
            };

            var executeCurrentSortAgain = function() {
                if ($scope.options.columns) {
                    var columnWithSort = null;
                    for (var i = 0; i < $scope.options.columns.length; i++) {
                        var column = $scope.options.columns[i];
                        if (column.isAscending === true
                            || column.isAscending === false) {
                            columnWithSort = column;
                            break;
                        }
                    }

                    if (columnWithSort !== null) {
                        sort(columnWithSort);
                    }

                }
            };

            var resetSortForAllExcepting = function (column) {
                for (var i = 0; i < $scope.options.columns.length; i++) {
                    if (column !== $scope.options.columns[i]) {
                        $scope.options.columns[i].isAscending = null;
                    }
                }
            };

            var sort = function (column) {
                switch (column.isAscending) {
                    case true:
                        $scope.rows.replaceElementsWith($filter("orderBy")($scope.rows, column.propertyName));
                        break;
                    case false:
                        $scope.rows.replaceElementsWith($filter("orderBy")($scope.rows, "-" + column.propertyName));
                        break;
                    default:
                        break;
                }
            };
        }]
    };
});

