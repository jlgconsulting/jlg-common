'use strict';
describe('jlg.common/treeDmSpecs tests:', function () {
    var scope,
    directiveElement,
    directiveIsolatedScope;

    beforeEach(function () {
        
        module("jlg.common");
        module("alltemplates");
        module("jlg.common");

        inject(["$rootScope", "$compile", function ($rootScope, $compile) {
            scope = $rootScope.$new();
            directiveElement = angular.element(
            "<tree-dm name-property='name' " +
                     "children-property='children' " +
                     "root-nodes-array='rootNodes' " +
                     "selected-node='selectedTreeNode'></tree-dm>");
            $compile(directiveElement)(scope);
            scope.$digest();
            directiveIsolatedScope = directiveElement.isolateScope();
        }]);
    });

    it("use example", function () {

        var level2Anode = {
            id: 2,
            name: "Level2 A",
        };
        scope.rootNodes = [{
            id: 0,
            name: "Root",
            children: [{
                id: 1,
                name: "Level1 A",
                children: [level2Anode, {
                    id: 3,
                    name: "Level2 B",
                }]
            }, {
                id: 4,
                name: "Level1 B",
                children: [{
                    id: 5,
                    name: "Level2 C",
                }, {
                    id: 6,
                    name: "Level2 D",
                }]
            }]
        }];
        scope.$digest();

        directiveIsolatedScope.selectedNode = level2Anode;
        directiveIsolatedScope.$digest();
        expect(directiveIsolatedScope.selectedNode).toBe(level2Anode);

    });
});