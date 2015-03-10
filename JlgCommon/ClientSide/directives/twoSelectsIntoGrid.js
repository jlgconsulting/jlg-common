var directivesModule = angular.module("directives");
directivesModule.directive("twoSelectsIntoGrid", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/twoSelectsIntoGrid.html"),
        scope: {
            selectedCombinedValues: "=",
            selectOptions1: "=",
            selectOptions2: "=",
            uniqueSelection1: "@",
            uniqueSelection2: "@",
            selectDisplayPropertyName1: "@",
            selectDisplayPropertyName2: "@",
            textSelectName1: "=",
            textSelectName2: "=",
            textColumnDisplayTitle1: "=",
            textColumnDisplayTitle2: "=",
            textDescription: "="
        },
        controller: ["$scope", "apfSharedDataAndPopupSrv",
            function ($scope, apfSharedDataAndPopupSrv) {

            $scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;
                    
                    $scope.gridDmOptions = {
                        columns: [],                        
                        deleteCallback: deleteSelectedCombinedValue,
                        deleteAllCallback: deleteAllSelectedCombinedValues,
                        textDelete: $scope.translatedText.delete,
                        textDeleteAll: $scope.translatedText.deleteAll
                    };
                }
            });

            var addSelectedCombinedValue = function() {
                if ($scope.uniqueSelection1 == "true") {
                    $scope.selectOptions1.remove($scope.selectedValue1);
                }

                if ($scope.uniqueSelection2 == "true") {
                    $scope.selectOptions2.remove($scope.selectedValue2);
                }

                $scope.selectedCombinedValues.push({
                    optionFromSelect1: $scope.selectedValue1,
                    optionFromSelect2: $scope.selectedValue2
                });

                $scope.selectedValue1 = null;
                $scope.selectedValue2 = null;
            };

            $scope.$watch("selectedValue1", function (newValue, oldValue) {
                if (newValue
                    && $scope.selectedValue2) {
                    addSelectedCombinedValue();
                }
            });

            $scope.$watch("selectedValue2", function (newValue, oldValue) {
                if (newValue
                    && $scope.selectedValue1) {
                    addSelectedCombinedValue();
                }
            });

            var deleteSelectedCombinedValue = function (deletedCombinedValue) {
                $scope.selectedCombinedValues.remove(deletedCombinedValue);
                
                if ($scope.uniqueSelection1=="true") {
                    $scope.selectOptions1.push(deletedCombinedValue.optionFromSelect1);
                }

                if ($scope.uniqueSelection2=="true") {
                    $scope.selectOptions2.push(deletedCombinedValue.optionFromSelect2);
                }
            };

            var deleteAllSelectedCombinedValues = function () {

                while ($scope.selectedCombinedValues.length > 0) {
                    deleteSelectedCombinedValue($scope.selectedCombinedValues[0])
                }                
            };
            
            var setUpgridDmColumns = function () {
                $scope.gridDmOptions.columns.length = 0;
                $scope.gridDmOptions.columns.push({
                    propertyName: "optionFromSelect1." + $scope.selectDisplayPropertyName1,
                    header: $scope.textColumnDisplayTitle1
                });
                $scope.gridDmOptions.columns.push({
                    propertyName: "optionFromSelect2." + $scope.selectDisplayPropertyName2,
                    header: $scope.textColumnDisplayTitle2
                });
            };

            $scope.$watch("textColumnDisplayTitle1", function (newValue, oldValue) {
                if (newValue && $scope.textColumnDisplayTitle2) {
                    
                    setUpgridDmColumns();
                }
            });

            $scope.$watch("textColumnDisplayTitle2", function (newValue, oldValue) {
                if (newValue && $scope.textColumnDisplayTitle1) {

                    setUpgridDmColumns();
                }
            });

        }]
    };
});

