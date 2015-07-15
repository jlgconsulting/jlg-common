var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("selectValuesWithAliasesIntoGrid", function ($filter) {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/directives/selectValuesWithAliasesIntoGrid.html"),
        scope: {
            selectDisplayPropertyName: "@",
            gridDisplayPropertyName: "@",
            selectOptions: "=",
            selectedValues: "=",
            textSelectName: "=",
            textColumnDisplayTitle: "=",
            hideAdd: "@"
        },
        controller: ["$scope", "sharedDataAndPopupSrv",
            function ($scope, sharedDataAndPopupSrv) {

            $scope.apfSharedData = sharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;

                    $scope.gridDmOptions = {
                        columns: [],                        
                        deleteCallback: $scope.deleteSelectedValue,
                        deleteAllCallback: $scope.deleteAllSelectedValues,
                        textDelete: $scope.translatedText.delete,
                        textDeleteAll: $scope.translatedText.deleteAll
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

            $scope.deleteAllSelectedValues = function () {
                while ($scope.selectedValues.length>0) {
                    $scope.deleteSelectedValue($scope.selectedValues[0])
                }
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

