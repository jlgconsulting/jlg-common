var directivesModule = angular.module("directives");
directivesModule.directive("selectValuesWithAliasesIntoGrid", function ($filter) {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/selectValuesWithAliasesIntoGrid.html"),
        scope: {
            selectDisplayPropertyName: "@",
            gridDisplayPropertyName: "@",
            selectOptions: "=",
            selectedValues: "=",
            textSelectName: "=",
            textColumnDisplayTitle: "="
        },
        controller: ["$scope", "apfSharedDataAndPopupSrv",
            function ($scope, apfSharedDataAndPopupSrv) {

            $scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;

                    $scope.gridDmOptions = {
                        columns: [],
                        deletes: true,
                        deleteCallback: $scope.deleteSelectedValue,
                        textDelete: $scope.translatedText.delete
                    };

                    if ($scope.gridDisplayPropertyName) {
                        $scope.gridDmOptions.columns.push({
                            propertyName: $scope.gridDisplayPropertyName,
                            header: $scope.textColumnDisplayTitle
                        });
                    } else {
                        $scope.gridDmOptions.columns.push({
                            propertyName: $scope.selectDisplayPropertyName,
                            header: $scope.textColumnDisplayTitle
                        });
                    }
                    
                }
            });
            
            $scope.$watch("selectedValue", function (newValue, oldValue) {
                if (newValue) {
                    if (!$scope.gridDisplayPropertyName) {
                        $scope.addSelectedValue();
                    } else {
                        $scope.customPropertyValue = newValue[$scope.selectDisplayPropertyName];
                    }
                } else {
                    $scope.customPropertyValue = null;
                }
            });

            $scope.deleteSelectedValue = function (deletedValue) {
                $scope.selectedValues.remove(deletedValue);
                if ($scope.gridDisplayPropertyName) {
                    delete deletedValue[$scope.gridDisplayPropertyName];
                }

                $scope.selectOptions.push(deletedValue);
            };
           
            $scope.addSelectedValue = function() {
                var selectedValue = angular.copy($scope.selectedValue);
                if ($scope.gridDisplayPropertyName) {
                    selectedValue[$scope.gridDisplayPropertyName] = $scope.customPropertyValue;
                }
                $scope.selectedValues.push(selectedValue);
                $scope.selectOptions.remove($scope.selectedValue);

                $scope.selectedValue = null;
                $scope.customPropertyValue = null;
            };

            $scope.addAll = function () {
                for (var i = 0; i < $scope.selectOptions.length; i++) {

                    var selectedValue = angular.copy($scope.selectOptions[i]);
                    if ($scope.gridDisplayPropertyName) {
                        selectedValue[$scope.gridDisplayPropertyName] = selectedValue[$scope.selectDisplayPropertyName];
                    }
                    $scope.selectedValues.push(selectedValue);
                }
                $scope.selectOptions.length = 0;
                $scope.customPropertyValue = null;
             };
            
        }]
    };
});

