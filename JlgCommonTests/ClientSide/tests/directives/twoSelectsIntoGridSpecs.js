'use strict';
describe('jlg.common/twoSelectsIntoGrid tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    var users = [{
        id: 1,
        name: "Ion",
        age: 23
    }, {
        id: 3,
        name: "Raluca",
        age: 20
    }, {
        id: 7,
        name: "Andra",
        age: 18
    }];

    var roles = [{
        id: 234,
        description: "Admin"
    }, {
        id: 100,
        description: "Normal"
    }];

    beforeEach(function () {
       
        module("jlg.common");
        module("alltemplates");
       
        inject(["$rootScope", "$compile", "globalSharedSrv", "arrayHelperSrv",
            function ($rootScope, $compile, globalSharedSrv, arrayHelperSrv) {
            scope = $rootScope.$new();
            scope.apfSharedData = globalSharedSrv.sharedData;
            scope.apfSharedData.loggedInContext = {};
            scope.apfSharedData.loggedInContext.translatedText = {};
            scope.customTextColumnDisplayTitle1 = "<b>User</b>";
            scope.customTextColumnDisplayTitle2 = "Role";
            scope.textSelectName1 = "Users available:";
            scope.textSelectName2 = "Users available:";
            scope.textDescription = "Users selected roles:";

            directiveElement = angular.element(
    "<two-selects-into-grid selected-combined-values='usersSelectedRoles' "+
            "select-options1='users' "+
            "select-options2='roles' "+
            "unique-selection1='true' "+
            "unique-selection2='false '"+
            "select-display-property-name1='name' "+
            "select-display-property-name2='description' "+
            "text-column-display-title1='customTextColumnDisplayTitle1' " +
            "text-column-display-title2='customTextColumnDisplayTitle2' " +
            "text-select-name1='textSelectName1' " +
            "text-select-name2='textSelectName2' " +
            "text-description='textDescription'></two-selects-into-grid>");

            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });

    it("addSelectedCombinedValue and deleteSelectedCombinedValue", function () {
        scope.users = users;
        scope.roles = roles;
        scope.usersSelectedRoles = [];
        scope.$digest();

        expect(directiveIsolatedScope.selectOptions1.length).toBe(3);
        expect(directiveIsolatedScope.selectOptions2.length).toBe(2);

        directiveIsolatedScope.selectedValue1 = users[0];
        directiveIsolatedScope.selectedValue2 = roles[1];
        directiveIsolatedScope.$digest();

        expect(directiveIsolatedScope.selectOptions1.length).toBe(2);
        expect(directiveIsolatedScope.selectOptions2.length).toBe(2);
        expect(scope.usersSelectedRoles).toEqual([{
            optionFromSelect1: {
                id: 1,
                name: "Ion",
                age: 23
            },
            optionFromSelect2: {
                id: 100,
                description: "Normal"
            }
        }]);
        
        directiveIsolatedScope.selectedValue1 = users[0];
        directiveIsolatedScope.selectedValue2 = roles[0];
        directiveIsolatedScope.$digest();

        expect(directiveIsolatedScope.selectOptions1.length).toBe(1);
        expect(directiveIsolatedScope.selectOptions2.length).toBe(2);
        expect(scope.usersSelectedRoles).toEqual([{
            optionFromSelect1: {
                id: 1,
                name: "Ion",
                age: 23
            },
            optionFromSelect2: {
                id: 100,
                description: "Normal"
            }
        }, {
            optionFromSelect1: {
                id: 3,
                name: "Raluca",
                age: 20
            },
            optionFromSelect2: {
                id: 234,
                description: "Admin"
            }
        }]);
        

        directiveIsolatedScope.gridDmOptions.deleteCallback(scope.usersSelectedRoles[1]);
        expect(directiveIsolatedScope.selectOptions1.length).toBe(2);
        expect(directiveIsolatedScope.selectOptions2.length).toBe(2);
        expect(scope.usersSelectedRoles).toEqual([{
            optionFromSelect1: {
                id: 1,
                name: "Ion",
                age: 23
            },
            optionFromSelect2: {
                id: 100,
                description: "Normal"
            }
        }]);
    });
});