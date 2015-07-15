var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("sectionTitle", function () {
    return {
        restrict: "E",
        templateUrl: window.urlGetter("ClientSide/directives/sectionTitle.html"),
        scope: {
            title: "=",
            goBack: "="
        },
        controller: ["$scope", "globalSharedSrv",
            function ($scope, globalSharedSrv) {

                $scope.globalSharedData = globalSharedSrv.sharedData;
                $scope.$watch("globalSharedData.loggedInContext", function (newValue) {
                    if (newValue) {
                        $scope.translatedText = newValue.translatedText;
                    }
                });
            }]
    };
});

