var directivesModule = angular.module("jlg.common.directives");
directivesModule.directive('ngHtmlCompile', ["$compile",
    function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.$watch(attrs.ngHtmlCompile, function (newValue, oldValue) {
                element.html(newValue);
                $compile(element.contents())(scope);
            });
        }
    }
}]);


//<div ng-html-compile="template"></div>

//<button ng-click="change()">Change</button>

//<ul>
//  <li>{{firstName}}</li>
//  <li>{{lastName}}</li>
//</ul>

//$scope.firstName = 'Bruce';
//$scope.lastName = 'Willis';

//$scope.template = 'First name : <input type="text" ng-model="firstName" />';

//$scope.change = function() {
//    $scope.template = 'Last Name : <input type="text" ng-model="lastName" />';
//};

