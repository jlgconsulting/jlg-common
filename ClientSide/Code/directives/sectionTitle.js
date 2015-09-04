var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("sectionTitle", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/sectionTitle.html"),
        scope: {
            title: "=",
            goBack: "="
        },
        controller: ["$scope", "globalSharedService",
            function ($scope, globalSharedService) {

                $scope.globalSharedData = globalSharedService.sharedData;
                $scope.$watch("globalSharedData.loggedInContext", function (newValue) {
                    if (newValue) {
                        $scope.translatedText = newValue.translatedText;
                    }
                });
            }]
    };
});

