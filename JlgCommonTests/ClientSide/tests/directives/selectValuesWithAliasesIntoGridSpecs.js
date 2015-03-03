'use strict';
describe('directives/selectValuesWithAliasesIntoGrid - simle without grid-display-property-name tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    var students = [{
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
    
    beforeEach(function () {
        module("shared");
        module("alltemplates");
        module("directives");

        inject(["$rootScope", "$compile", "apfSharedDataAndPopupSrv", "arrayHelperSrv",
            function ($rootScope, $compile, apfSharedDataAndPopupSrv, arrayHelperSrv) {
            scope = $rootScope.$new();
            scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            scope.apfSharedData.loggedInContext = {};
            scope.apfSharedData.loggedInContext.translatedText = {};

            scope.students = students;
            scope.selectedStudentsInClass = [];
            scope.textAboveSelect = "Students";
            scope.textAboveGrid = "Selected students ages";

            directiveElement = angular.element(
                "<select-values-with-aliases-into-grid " +
                    "select-display-property-name='name' " +
                    "select-options='students' " +
                    "selected-values='selectedStudentsInClass' " +
                    "text-select-name='textAboveSelect' " +
                    "text-column-display-title='textAboveGrid'></select-values-with-aliases-into-grid>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });

    it("addSelectedValue and deleteSelectedValue function", function () {
        expect(directiveIsolatedScope.selectOptions).toEqual(students);
        expect(scope.selectedStudentsInClass).toEqual([]);

        directiveIsolatedScope.selectedValue = students[1];
        directiveIsolatedScope.addSelectedValue();

        expect(directiveIsolatedScope.selectedValue).toBe(null);
        
        expect(scope.selectedStudentsInClass).toEqual([{
            id: 3,
            name: "Raluca",
            age: 20
        }]);

        expect(directiveIsolatedScope.selectOptions).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        }, {
            id: 7,
            name: "Andra",
            age: 18
        }]);

        directiveIsolatedScope.deleteSelectedValue(scope.selectedStudentsInClass[0]);
        directiveIsolatedScope.$digest();
        expect(scope.selectedStudentsInClass).toEqual([]);
        expect(directiveIsolatedScope.selectOptions).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        }, {
            id: 7,
            name: "Andra",
            age: 18
        }, {
            id: 3,
            name: "Raluca",
            age: 20
        }]);
    });

    it("addAll function", function () {
        expect(directiveIsolatedScope.selectOptions).toEqual(students);
        expect(scope.selectedStudentsInClass).toEqual([]);
        directiveIsolatedScope.addAll();
        expect(directiveIsolatedScope.selectedValues).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        }, {
            id: 7,
            age: 18,
            name: "Andra"
        }, {
            id: 3,
            name: "Raluca",
            age: 20
        }]);
        expect(scope.selectedStudentsInClass).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        }, {
            id: 7,
            age: 18,
            name: "Andra"
        }, {
            id: 3,
            name: "Raluca",
            age: 20
        }]);
        expect(directiveIsolatedScope.selectOptions).toEqual([]);
    });
});

describe('_directives/shared/selectValuesWithAliasesIntoGrid with grid-display-property-name tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;
    
    var students = [{
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

    beforeEach(function () {
        module("shared");
        module("alltemplates");
        module("directives");

        inject(function ($rootScope, $compile, apfSharedDataAndPopupSrv) {
            scope = $rootScope.$new();
            scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            scope.apfSharedData.loggedInContext = {};
            scope.apfSharedData.loggedInContext.translatedText = {};
            
            scope.students = students;
            scope.selectedStudentsInClass = [];
            scope.textAboveSelect = "Students";
            scope.textAboveGrid = "Selected students ages";

            directiveElement = angular.element(
                "<select-values-with-aliases-into-grid "+ 
                    "select-display-property-name='name' "+
                    "grid-display-property-name='nick' " +
                    "select-options='students' " +
                    "selected-values='selectedStudentsInClass' " +
                    "text-select-name='textAboveSelect' " +
                    "text-column-display-title='textAboveGrid'></select-values-with-aliases-into-grid>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        });
    });
    
    it("addSelectedValue and deleteSelectedValue function", function () {
        expect(directiveIsolatedScope.selectOptions).toEqual(students);
        expect(scope.selectedStudentsInClass).toEqual([]);

        directiveIsolatedScope.selectedValue = students[1];
        directiveIsolatedScope.customPropertyValue = "@Ralu";
        
        directiveIsolatedScope.addSelectedValue();
        expect(directiveIsolatedScope.selectedValue).toBe(null);
        expect(directiveIsolatedScope.customPropertyValue).toBe(null);
        expect(scope.selectedStudentsInClass).toEqual([{
            id: 3,
            name: "Raluca",
            age: 20,
            nick: "@Ralu"
        }]);
        
        expect(directiveIsolatedScope.selectOptions).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        },{
            id: 7,
            name: "Andra",
            age: 18
        }]);
        
        directiveIsolatedScope.deleteSelectedValue(scope.selectedStudentsInClass[0]);
        directiveIsolatedScope.$digest();
        expect(scope.selectedStudentsInClass).toEqual([]);
        expect(directiveIsolatedScope.selectOptions).toEqual([{
            id: 1,
            name: "Ion",
            age: 23
        }, {
            id: 7,
            name: "Andra",
            age: 18
        }, {
            id: 3,
            name: "Raluca",
            age: 20
        }]);
    });
   
    it("addAll function", function () {
        expect(directiveIsolatedScope.selectOptions).toEqual(students);
        expect(scope.selectedStudentsInClass).toEqual([]);
        directiveIsolatedScope.addAll();
        expect(scope.selectedStudentsInClass).toEqual([{
            id: 1,
            name: "Ion",
            nick: "Ion",
            age: 23
        }, {
            id: 7,
            age: 18,
            name: "Andra",
            nick: "Andra"
        }, {
            id: 3,
            name: "Raluca",
            age: 20,
            nick: "Raluca"
        }]);
        expect(directiveIsolatedScope.selectOptions).toEqual([]);
    });
});