"use strict";
describe("arrayHelperSrv tests", function () {
    var arrayHelperSrv4Test;

    //excuted before each "it" is run.
    beforeEach(function () {

        //load the module.
        module("shared");

        //inject your dateHelperSrv for testing.
        inject(["arrayHelperSrv", function (arrayHelperSrv) {
            arrayHelperSrv4Test = arrayHelperSrv;
        }]);
    });

    it("createObjectArrayFromStringArray: should work", function () {
        expect(arrayHelperSrv4Test.createObjectArrayFromStringArray(["BMW", "DACIA", "SKODA"], "car"))
            .toEqual([{
                car: "BMW"
            }, {
                car: "DACIA"
            }, {
                car: "SKODA"
            }]);
    });

    it("createNewArraysSelectSyncronizer: should work", function () {

        var cezar = {
            name: "Cezar",
            age: 50
        };
        var delia = {
            name: "Delia",
            age: 20
        };
        var ion = {
            name: "Ion",
            age: 40
        };
        var costel = {
            name: "Costel",
            age: 15
        };
        
        var arraysOptionsContainerObject = {
        };
        var arrayNames = ["groupA", "groupB", "groupC"];

        var initCommonArrayValues = [cezar, delia, ion, costel];
        
        var elementIdProperty = "name";
        var elementOrderByProperty = "age";

        var arraysSelectSyncronizer =
            arrayHelperSrv4Test.createNewArraysSelectSyncronizer
                (arraysOptionsContainerObject,
                 arrayNames,
                 initCommonArrayValues,
                 elementIdProperty,
                 elementOrderByProperty);

        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, ion, cezar]);
        
        var ileana = {
            name: "Ileana",
            age: 23
        };
        arraysSelectSyncronizer.addToAllButThis("groupA", ileana);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia, ileana, ion, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, ileana, ion, cezar]);
        
        arraysSelectSyncronizer.removeFromAllButThis("groupB", ileana);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia, ileana, ion, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, ion, cezar]);
        
        arraysSelectSyncronizer.removeFromAllButThis("groupA", ileana);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, ion, cezar]);
        
        arraysSelectSyncronizer.syncAllAfterSelection("groupA", delia, null);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, ion, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, ion, cezar]);
        
        arraysSelectSyncronizer.syncAllAfterSelection("groupA", ion, delia);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, cezar]);
        
        arraysSelectSyncronizer.syncAllAfterSelection("groupC", cezar, null);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, delia, ion]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, delia]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, cezar]);
        
        arraysSelectSyncronizer.syncAllAfterSelection("groupC", delia, cezar);
        expect(arraysOptionsContainerObject.groupA).toEqual([costel, ion, cezar]);
        expect(arraysOptionsContainerObject.groupB).toEqual([costel, cezar]);
        expect(arraysOptionsContainerObject.groupC).toEqual([costel, delia, cezar]);
    });
    
    var sampleArray = [{
        id: 2,
        name: "Ion",
        age: 45
    }, {
        id: 5,
        name: "Maria",
        age: 19
    }, {
        id: 11,
        name: "Petre",
        age: 22
    }, {
        id: 17,
        name: "George",
        age: 67
    }];

    it("getById: should work", function () {
        expect(sampleArray.getById(11)).toEqual({
            id: 11,
            name: "Petre",
            age: 22
        });

        expect(sampleArray.getById("Maria", "name")).toEqual({
            id: 5,
            name: "Maria",
            age: 19
        });

        expect(sampleArray.getById(67, "age")).toEqual({
            id: 17,
            name: "George",
            age: 67
        });

    });

    it("getAllIds: should work", function () {
        expect(sampleArray.getAllIds()).toEqual([2, 5, 11, 17]);
        expect(sampleArray.getAllIds("name")).toEqual(["Ion", "Maria", "Petre", "George"]);
    });

    it("filterByOtherArrayIds: should work", function () {
        var sampleOtherArray = [{
            id: 5,
            brand: "Porche",
            owner: "George"
        }, {
            id: 6,
            brand: "Seat",
            owner: "Maria"
        }, {
            id: 17,
            brand: "Dacia",
            owner: "Ion"
        }];

        expect(sampleArray.filterByOtherArrayIds(sampleOtherArray)).toEqual([{
            id: 5,
            name: "Maria",
            age: 19
        }, {
            id: 17,
            name: "George",
            age: 67
        }]);

        expect(sampleArray.filterByOtherArrayIds(sampleOtherArray, true)).toEqual([{
            id: 2,
            name: "Ion",
            age: 45
        }, {
            id: 11,
            name: "Petre",
            age: 22
        }]);

        expect(sampleArray.filterByOtherArrayIds(sampleOtherArray, false, "name", "owner")).toEqual([{
            id: 2,
            name: "Ion",
            age: 45
        }, {
            id: 5,
            name: "Maria",
            age: 19
        }, {
            id: 17,
            name: "George",
            age: 67
        }]);

        expect(sampleArray.filterByOtherArrayIds(sampleOtherArray, true, "name", "owner")).toEqual([{
            id: 11,
            name: "Petre",
            age: 22
        }]);
    });

    it("filterArrayByIds: should work", function () {
        expect(sampleArray.filterArrayByIds([11, 17, 2])).toEqual([{
            id: 2,
            name: "Ion",
            age: 45
        }, {
            id: 11,
            name: "Petre",
            age: 22
        }, {
            id: 17,
            name: "George",
            age: 67
        }]);

        expect(sampleArray.filterArrayByIds([11, 17, 2], true)).toEqual([{
            id: 5,
            name: "Maria",
            age: 19
        }]);

        expect(sampleArray.filterArrayByIds(["Petre", "Maria"], false, "name")).toEqual([{
            id: 5,
            name: "Maria",
            age: 19
        }, {
            id: 11,
            name: "Petre",
            age: 22
        }]);

        expect(sampleArray.filterArrayByIds(["Petre", "Maria"], true, "name")).toEqual([{
            id: 2,
            name: "Ion",
            age: 45
        }, {
            id: 17,
            name: "George",
            age: 67
        }]);

    });

    it("pushElementAtIndex: should work", function () {
        var array = ["3", "5", "7", "9", "13"];
        expect(array.length).toBe(5);

        array.pushElementAtIndex("11", 4);
        expect(array.length).toBe(6);
        expect(array[4]).toBe("11");

        array.pushElementAtIndex("1", -2);
        expect(array.length).toBe(7);
        expect(array[0]).toBe("1");

        array.pushElementAtIndex("17", 167);
        expect(array.length).toBe(8);
        expect(array[7]).toBe("17");

        array.pushElementAtIndex("15", 7);
        expect(array.length).toBe(9);
        expect(array[7]).toBe("15");

    });

    var john = {
        id: 456,
        name: "John"
    };
    var mary = {
        id: 80001,
        name: "Mary"
    };

    it("remove: should work", function () {
        expect(sampleArray.length).toBe(4);
        sampleArray.push(john);
        expect(sampleArray.length).toBe(5);

        expect(sampleArray.remove(john)).toBe(4);
        expect(sampleArray.length).toBe(4);

        expect(sampleArray.remove(mary)).toBe(-1);
        expect(sampleArray.length).toBe(4);
    });

    it("removeById: should work", function () {
        expect(sampleArray.length).toBe(4);
        sampleArray.push(john);
        sampleArray.push(mary);
        expect(sampleArray.length).toBe(6);

        expect(sampleArray.removeById(456)).toBe(4);
        expect(sampleArray.length).toBe(5);

        expect(sampleArray.removeById("Mary", "name")).toBe(4);
        expect(sampleArray.length).toBe(4);

        expect(sampleArray.removeById(38756535)).toBe(-1);
        expect(sampleArray.length).toBe(4);

        expect(sampleArray.removeById("dsjgfjsfhi53217nb", "name")).toBe(-1);
        expect(sampleArray.length).toBe(4);
    });

    it("removeAllElementsThatContain: should work", function () {
        expect(sampleArray.length).toBe(4);
        sampleArray.push(john);
        sampleArray.push(mary);
        expect(sampleArray.length).toBe(6);

        expect(sampleArray.removeAllElementsThatContain("hn", "name"))
            .toEqual([{
                id: 456,
                name: "John"
            }]);
        expect(sampleArray.length).toBe(5);

        expect(sampleArray.removeAllElementsThatContain("001", "id"))
            .toEqual([{
                id: 80001,
                name: "Mary"
            }]);
        expect(sampleArray.length).toBe(4);
    });

    it("removeAllElementsAfterIndex: should work", function () {
        expect(sampleArray.length).toBe(4);
        sampleArray.push(john);
        sampleArray.push(mary);
        expect(sampleArray.length).toBe(6);

        expect(sampleArray.removeAllElementsAfterIndex(3)).toEqual([{
            id: 456,
            name: "John"
        }, {
            id: 80001,
            name: "Mary"
        }]);
        expect(sampleArray.length).toBe(4);
    });

    it("replaceElement: should work", function () {
        var array = [];

        array.push(john);
        expect(array.length).toBe(1);
        expect(array[0]).toEqual({
            id: 456,
            name: "John"
        });

        array.replaceElement(john, mary);

        expect(array[0]).toEqual({
            id: 80001,
            name: "Mary"
        });
        expect(array.length).toBe(1);
    });

    it("replaceElementsWith: should work", function () {
        var array = [1, 4, 6, 8, 9, 12, 13];
        var otherArray = ["first", "second", "third"];

        expect(array.length).toBe(7);
        expect(otherArray.length).toBe(3);

        array.replaceElementsWith(otherArray);

        expect(array.length).toBe(3);
        expect(otherArray.length).toBe(3);

        expect(array).toEqual(otherArray);
        expect(array).not.toBe(otherArray);
    });

});
