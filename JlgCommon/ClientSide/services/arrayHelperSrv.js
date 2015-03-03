var sharedModule = angular.module("shared");
sharedModule.factory("arrayHelperSrv",
    ["$filter",
    function ($filter) {

    Array.prototype.getById = function (elementId, idPropertyCustomName) {
        if (!idPropertyCustomName) {
            idPropertyCustomName = "id";
        }

        for (var i = 0; i < this.length; i++) {
            if (this[i][idPropertyCustomName] == elementId) {
                return this[i];
            }
        }
        return null;
    };

    Array.prototype.getAllIds = function (idPropertyCustomName) {
        if (!idPropertyCustomName) {
            idPropertyCustomName = "id";
        }

        var ids = [];
        for (var i = 0; i < this.length; i++) {
            ids.push(this[i][idPropertyCustomName]);
        }
        return ids;
    };

    Array.prototype.filterByOtherArrayIds = function (otherArray, notContained, idPropertyCustomNameArray, idPropertyCustomNameOtherArray) {
        if (!notContained) {
            notContained = false;
        }

        if (!idPropertyCustomNameArray) {
            idPropertyCustomNameArray = "id";
        }

        if (!idPropertyCustomNameOtherArray) {
            idPropertyCustomNameOtherArray = "id";
        }

        var arrayFiltered = [];
        for (var i = 0; i < this.length; i++) {
            var isNotContained = true;
            for (var j = 0; j < otherArray.length; j++) {
                if (this[i][idPropertyCustomNameArray] == otherArray[j][idPropertyCustomNameOtherArray]) {
                    isNotContained = false;
                    break;
                }
            }
            if (isNotContained == notContained) {
                arrayFiltered.push(this[i]);
            }
        }

        return arrayFiltered;
    };

    Array.prototype.filterArrayByIds = function (ids, notContained, idPropertyCustomName) {
        if (!notContained) {
            notContained = false;
        }

        if (!idPropertyCustomName) {
            idPropertyCustomName = "id";
        }

        var arrayFiltered = [];
        for (var i = 0; i < this.length; i++) {
            var isNotContained = true;
            for (var j = 0; j < ids.length; j++) {
                if (this[i][idPropertyCustomName] == ids[j]) {
                    isNotContained = false;
                    break;
                }
            }
            if (isNotContained == notContained) {
                arrayFiltered.push(this[i]);
            }
        }

        return arrayFiltered;
    };

    Array.prototype.pushElementAtIndex = function (element, index) {
        if (index < 0) {
            this.splice(0, 0, element);
            return;
        }

        if (index >= this.length) {
            this.push(element);
            return;
        }

        this.splice(index, 0, element);
    };

    Array.prototype.remove = function (element) {
        var index = this.indexOf(element);
        if (index != -1) {
            this.splice(index, 1);
        }
        return index;
    };

    Array.prototype.removeById = function (elementId, idPropertyCustomName) {
        if (!idPropertyCustomName) {
            idPropertyCustomName = "id";
        }

        var indexElement = -1;
        for (var i = 0; i < this.length; i++) {
            if (this[i][idPropertyCustomName] == elementId) {
                indexElement = i;
                break;
            }
        }

        if (indexElement != -1) {
            this.splice(indexElement, 1);
        }

        return indexElement;
    };

    Array.prototype.removeAllElementsThatContain = function (containedString, propertyName) {
        var elementsToBeDeleted = [];
        for (var i = 0; i < this.length; i++) {
            if (this[i][propertyName]
                && ((this[i][propertyName] + "").indexOf(containedString) > -1)) {
                elementsToBeDeleted.push(this[i]);
            }
        }

        for (var i = 0; i < elementsToBeDeleted.length; i++) {
            this.remove(elementsToBeDeleted[i]);
        }

        return elementsToBeDeleted;
    };

    Array.prototype.removeAllElementsAfterIndex = function (index) {

        var elementsToBeDeleted = [];
        for (var i = index + 1; i < this.length; i++) {
            elementsToBeDeleted.push(this[i]);
        }

        for (var i = 0; i < elementsToBeDeleted.length; i++) {
            this.remove(elementsToBeDeleted[i]);
        }
        return elementsToBeDeleted;
    };

    Array.prototype.replaceElement = function (oldElement, newElement) {
        var index = this.indexOf(oldElement);
        if (index != -1) {
            this[index] = newElement;
        }
    };

    Array.prototype.replaceElementsWith = function (newArray) {
        this.length = 0;
        for (var i = 0; i < newArray.length; i++) {
            this.push(newArray[i]);
        }
    };

    var createObjectArrayFromStringArray = function (stringArray, property) {
        var objectArray = [];
        for (var i = 0; i < stringArray.length; i++) {
            var obj = {};
            obj[property] = stringArray[i];
            objectArray.push(obj);
        }
        return objectArray;
    };
    
    var arraysSelectSyncronizer = function (arraysOptionsContainerObject, arrayNames, initArrayValues, elementIdProperty, elementOrderByProperty) {
        if (!elementOrderByProperty) {
            elementOrderByProperty = elementIdProperty;
        } else {
            initArrayValues = $filter("orderBy")(initArrayValues, elementOrderByProperty);
        }

        var reset = function () {
            for (var i = 0; i < arrayNames.length; i++) {
                arraysOptionsContainerObject[arrayNames[i]] = angular.copy(initArrayValues);
            }
        };
        reset();

        var addToAllButThis = function (thisArrayName, element) {
            for (var arrayName in arraysOptionsContainerObject) {
                if (arrayName != thisArrayName) {
                    var existingElement = arraysOptionsContainerObject[arrayName].getById(element[elementIdProperty], elementIdProperty);
                    if (!existingElement) {
                        arraysOptionsContainerObject[arrayName].push(element);
                        arraysOptionsContainerObject[arrayName] = $filter("orderBy")(arraysOptionsContainerObject[arrayName], elementOrderByProperty);
                    }
                }
            }
        };

        var removeFromAllButThis = function (thisArrayName, element) {
            for (var arrayName in arraysOptionsContainerObject) {
                if (arrayName != thisArrayName) {
                    var removedElement = arraysOptionsContainerObject[arrayName].getById(element[elementIdProperty], elementIdProperty);
                    if (removedElement) {
                        arraysOptionsContainerObject[arrayName].remove(removedElement);
                    }
                }
            }
        };

        var syncAllAfterSelection = function (modifiedArrayName, newSelectedValue, oldSelectedValue) {
            if (newSelectedValue) {
                removeFromAllButThis(modifiedArrayName, newSelectedValue);
            }
            if (oldSelectedValue) {
                addToAllButThis(modifiedArrayName, oldSelectedValue);
            }
        };
        
        return {
            addToAllButThis: addToAllButThis,
            reset: reset,
            removeFromAllButThis: removeFromAllButThis,
            syncAllAfterSelection: syncAllAfterSelection
        };
    };
   
    return {
        createObjectArrayFromStringArray: createObjectArrayFromStringArray,
        createNewArraysSelectSyncronizer: function (arraysOptionsContainerObject, arrayNames, initCommonArrayValues, elementIdProperty, elementOrderByProperty) {
            return new arraysSelectSyncronizer(arraysOptionsContainerObject, arrayNames, initCommonArrayValues, elementIdProperty, elementOrderByProperty);
        }
    };
}]);
