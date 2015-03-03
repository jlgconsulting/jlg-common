var directivesModule = angular.module("directives");
directivesModule.directive("sectionTitle", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/sectionTitle.html"),
        scope: {
            title: "=",
            goBack: "="
        },
        controller: ["$scope", "apfSharedDataAndPopupSrv",
            function ($scope, apfSharedDataAndPopupSrv) {

                $scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
                $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                    if (newValue) {
                        $scope.translatedText = newValue.translatedText;
                    }
                });
            }]
    };
});

