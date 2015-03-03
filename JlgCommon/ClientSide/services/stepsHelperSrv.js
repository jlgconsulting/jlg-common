var sharedModule = angular.module("shared");
sharedModule.factory('stepsHelperSrv',
    [
    function () {

    var stepsHelper = function (firstStep, lastStep, conditionsRequiredForSteps, customcallBacksForSteps) {
        var sharedData = {
            currentStep: firstStep,
            firstStep: firstStep,
            lastStep: lastStep
        };

        var stepPassesConditions = function (step) {
            if (!conditionsRequiredForSteps[step]
                || conditionsRequiredForSteps[step]()) {
                return true;
            }
            return false;
        };
          
        var allStepsPassConditions = function () {
            for (var step = firstStep; step <= lastStep; step++) {
                if (!stepPassesConditions(step)) {
                    return false;
                }
            }

            return true;
        };

        var goToStep = function (step) {
            sharedData.currentStep = step;
            if (customcallBacksForSteps[step]) {
                customcallBacksForSteps[step]();
            }
        };
        
        var canGoToPreviousStep = function () {
            if (sharedData.currentStep > firstStep) {
                return true;
            } else {
                return false;
            }
        };

        var goToPreviousStep = function () {
            goToStep(sharedData.currentStep -1);
        };

        var canGoToNextStep = function () {
            if (stepPassesConditions(sharedData.currentStep)
                && sharedData.currentStep < lastStep) {
                return true;
            } else {
                return false;
            }
        };

        var goToNextStep = function () {
            goToStep(sharedData.currentStep + 1);

        };

        return {
            sharedData: sharedData,
            stepPassesConditions: stepPassesConditions,
            allStepsPassConditions: allStepsPassConditions,
            goToStep: goToStep,
            canGoToPreviousStep: canGoToPreviousStep,
            goToPreviousStep: goToPreviousStep,
            canGoToNextStep: canGoToNextStep,
            goToNextStep: goToNextStep
        };
    };

    return {
        createNew: function (firstStep, lastStep, conditionsRequiredForSteps, customcallBacksForSteps) {
            return new stepsHelper(firstStep, lastStep, conditionsRequiredForSteps, customcallBacksForSteps);
        }
    };
}]);
