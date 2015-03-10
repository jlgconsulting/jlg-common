'use strict';
describe('_directives/shared/gridDm tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    var users = [{
        id: 1,
        name: "Ion",
        age: 50
    }, {
        id: 3,
        name: "Raluca",
        age: 30
    }, {
        id: 7,
        name: "Andra",
        age: 20
    }, {
        id: 10,
        name: "Petre",
        age: 70
    }, {
        id: 11,
        name: "Marius",
        age: 40
    }];
    
    beforeEach(function () {
        module("shared");
        module("alltemplates");
        module("directives");

        inject(["$rootScope", "$compile", function ($rootScope, $compile) {
            scope = $rootScope.$new();
            directiveElement = angular.element(
                 "<grid-dm rows='gridDmRows'" +
                 "options='gridDmOptions'" +
                 "table-class='custom-table-class'></grid-dm>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });
    
    it("use example - with default buttons for update and delete + tests for toggleSort", function () {
        var calledUserCreate = false;
        var calledUserUpdate = false;
        var calledUserDelete = false;
        var createUpdateUser = function (user) {
            if (!user) {
                calledUserCreate = true;
            } else {
                calledUserUpdate = true;
            }
        };
        
        var deleteUser = function (user) {
            calledUserDelete = true;
        };

        scope.gridDmOptions = {
            columns: [{
                propertyName: "id",
                header: "<b>User's id</b>"
            }, {
                propertyName: "name",
                header: "User's name"
            }, {
                propertyName: "age",
                header: "<i class='fa fa-info'></i> User's age",
                cellTemplate: function(row) {
                    if (row.id>4) {
                        return row.age;
                    } else {
                        return "<i class='fa fa-info'></i> Age is hidden for id<=4!";
                    }
                }
            }],            
            createCallback: createUpdateUser,
            updateCallback: createUpdateUser,
            deleteCallback: deleteUser,
            //disabled callBacks are optional. Is none is provided than the button is always enabled
            createDisabledCallback: function () {
                return false;
            },
            updateDisabledCallback: function (user) {
                return user.id == 7;
            },
            deleteDisabledCallback: function (user) {
                return user.id == 3;
            }
        };
        scope.gridDmRows = users;
        scope.$digest();
        
        expect(directiveIsolatedScope.rows).toEqual(scope.gridDmRows);
        expect(directiveIsolatedScope.options).toEqual(scope.gridDmOptions);

        expect(directiveIsolatedScope.options.createDisabledCallback()).toBe(false);

        expect(directiveIsolatedScope.options.updateDisabledCallback(users[2])).toBe(true);
        expect(directiveIsolatedScope.options.updateDisabledCallback(users[0])).toBe(false);

        expect(directiveIsolatedScope.options.deleteDisabledCallback(users[1])).toBe(true);
        expect(directiveIsolatedScope.options.deleteDisabledCallback(users[0])).toBe(false);
        
        directiveIsolatedScope.options.createCallback();
        expect(calledUserCreate).toBe(true);
        
        directiveIsolatedScope.options.updateCallback(users[1]);
        expect(calledUserUpdate).toBe(true);
        
        directiveIsolatedScope.options.deleteCallback(users[3]);
        expect(calledUserDelete).toBe(true);
        
        //column sort
        //toggle sort by name
        expect(directiveIsolatedScope.rows[0]).toEqual({
            id: 1,
            name: "Ion",
            age: 50
        });

        directiveIsolatedScope.toggleSort(scope.gridDmOptions.columns[1]);
        expect(directiveIsolatedScope.rows[0]).toEqual({
            id: 7,
            name: "Andra",
            age: 20
        });

        directiveIsolatedScope.toggleSort(scope.gridDmOptions.columns[1]);
        expect(directiveIsolatedScope.rows[0]).toEqual({
            id: 3,
            name: "Raluca",
            age: 30
        });
        
        //toggle sort by age
        directiveIsolatedScope.toggleSort(scope.gridDmOptions.columns[2]);
        expect(directiveIsolatedScope.rows[0]).toEqual({
            id: 7,
            name: "Andra",
            age: 20
        });
        
        directiveIsolatedScope.toggleSort(scope.gridDmOptions.columns[2]);
        expect(directiveIsolatedScope.rows[0]).toEqual({
            id: 10,
            name: "Petre",
            age: 70
        });
        
    });
    
    it("use example - with custom buttons for rows", function () {
        var calledAction1 = false;
        var calledAction2 = false;
        var calledAction3 = false;
        
        var performAction1 = function (user) {
            calledAction1 = true;
        };
        var performAction2 = function (user) {
            calledAction2 = true;
        };
        var performAction3 = function (user) {
            calledAction3 = true;
        };

        scope.gridDmOptions = {
            columns: [{
                propertyName: "id",
                header: "<b>User's id</b>"
            }, {
                propertyName: "name",
                header: "User's name"
            }, {
                propertyName: "age",
                header: "<i class='fa fa-info'></i> User's age",
                cellTemplate: function (row) {
                    if (row.id > 4) {
                        return row.age;
                    } else {
                        return "<i class='fa fa-info'></i> Age is hidden for id<=4!";
                    }
                }
            }],
            customButtons: [{
                name: "tooltip for action1",
                template: "<i class='fa fa-edit'></i>",
                customClass: "btn btn-default",
                disabledCallback: function(user) {
                    return user.id == 1;
                },
                clickCallback: performAction1
            }, {
                name: "tooltip for action2",
                template: "<i class='fa fa-calendar'></i>",
                customClass: "btn btn-default",
                clickCallback: performAction2
            }, {
                name: "tooltip for action3",
                template: "<i class='fa fa-trash-o'></i>",
                customClass: "btn btn-danger",
                clickCallback: performAction3,
                disabledCallback: function (user) {
                    return user.id == 7;
                },
            }]
        };
        scope.gridDmRows = users;
        scope.$digest();

        expect(directiveIsolatedScope.rows).toEqual(scope.gridDmRows);
        expect(directiveIsolatedScope.options).toEqual(scope.gridDmOptions);

        expect(directiveIsolatedScope.options.customButtons[0].disabledCallback(users[0])).toBe(true);
        expect(directiveIsolatedScope.options.customButtons[0].disabledCallback(users[1])).toBe(false);

        expect(directiveIsolatedScope.options.customButtons[1].disabledCallback).toBe(undefined);

        expect(directiveIsolatedScope.options.customButtons[2].disabledCallback(users[2])).toBe(true);
        expect(directiveIsolatedScope.options.customButtons[2].disabledCallback(users[0])).toBe(false);
        

        directiveIsolatedScope.options.customButtons[0].clickCallback(users[1]);
        expect(calledAction1).toBe(true);

        directiveIsolatedScope.options.customButtons[1].clickCallback(users[1]);
        expect(calledAction2).toBe(true);
        
        directiveIsolatedScope.options.customButtons[2].clickCallback(users[1]);
        expect(calledAction2).toBe(true);
    });
    
    it("tableClass", function () {
        expect(directiveIsolatedScope.tableClass).toBe("custom-table-class");
    });

    it("getDeepPropertyValueFromStringPath function", function () {
        expect(directiveIsolatedScope.getDeepPropertyValueFromStringPath({
            programmer: { name: "Dan" }
        }, "programmer.name"))
        .toBe("Dan");

        expect(directiveIsolatedScope.getDeepPropertyValueFromStringPath({
            age: 25,
            country: {
                region: {
                    village: {
                        name: "Polovragi"
                    }
                }
            }
        }, "country.region.village.name"))
        .toBe("Polovragi");
    });
});