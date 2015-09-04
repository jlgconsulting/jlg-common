"use strict";
describe("jlg.common/stepsHelperSrv tests", function () {
    var stepsHelperSrv4Test;

    //excuted before each "it" is run.
    beforeEach(function () {

        //load the module.
        module("jlg.common");
        
        //inject your dateHelperSrv for testing.
        inject(["stepsHelperSrv", function (stepsHelperSrv) {
            stepsHelperSrv4Test = stepsHelperSrv;
        }]);
    });

    it("stepsHelperSrv: should work", function () {
        var steps = {            
            First: 0,
            Second: 1,
            Third: 2,
            Fourth: 3
        };
        
        var age = 25;
        var variableModifiedAtStepInsideCallBack = 3;

        var conditionsRequiredForSteps = [];
        conditionsRequiredForSteps[steps.Second] = function() {
            if (age > 30) {
                return true;
            }
            return false;
        };
        conditionsRequiredForSteps[steps.Third] = function() {
            if (age < 22) {
                return true;
            }
            return false;
        };

        var customcallBacksForSteps = [];
        customcallBacksForSteps[steps.First] = function () {
            variableModifiedAtStepInsideCallBack = 100;
        };
        customcallBacksForSteps[steps.Third] = function () {
            variableModifiedAtStepInsideCallBack = -15;
        };

        var stepsHelper = stepsHelperSrv4Test.createNew(steps.First, steps.Fourth, conditionsRequiredForSteps, customcallBacksForSteps);
        expect(stepsHelper).not.toBe(null);
        expect(stepsHelper.sharedData.firstStep).toEqual(steps.First);
        expect(stepsHelper.sharedData.lastStep).toEqual(steps.Fourth);

        expect(variableModifiedAtStepInsideCallBack).toBe(3);
        stepsHelper.goToStep(steps.First);
        expect(stepsHelper.sharedData.currentStep).toEqual(steps.First);
        expect(variableModifiedAtStepInsideCallBack).toBe(100);
        expect(stepsHelper.canGoToPreviousStep()).toBe(false);
        expect(stepsHelper.canGoToNextStep()).toBe(true);

        stepsHelper.goToNextStep();
        expect(variableModifiedAtStepInsideCallBack).toBe(100);
        expect(stepsHelper.sharedData.currentStep).toEqual(steps.Second);
        expect(stepsHelper.canGoToPreviousStep()).toBe(true);
        expect(age).toBe(25);
        expect(stepsHelper.canGoToNextStep()).toBe(false);
        age = 40;
        expect(stepsHelper.canGoToNextStep()).toBe(true);
        
        stepsHelper.goToNextStep();
        expect(variableModifiedAtStepInsideCallBack).toBe(-15);
        expect(stepsHelper.sharedData.currentStep).toEqual(steps.Third);
        expect(stepsHelper.canGoToPreviousStep()).toBe(true);
        expect(age).toBe(40);
        expect(stepsHelper.canGoToNextStep()).toBe(false);
        age = 21;
        expect(stepsHelper.canGoToNextStep()).toBe(true);


        stepsHelper.goToStep(steps.First);
        expect(variableModifiedAtStepInsideCallBack).toBe(100);
        expect(stepsHelper.sharedData.currentStep).toEqual(steps.First);
        expect(stepsHelper.canGoToPreviousStep()).toBe(false);
    });
});
