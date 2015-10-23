var jlgCommonModule = angular.module("jlg.common");
jlgCommonModule.directive("confirmPopup", function () {
    return {
        restrict: "E",
        templateUrl: window.serverAppPath("ClientSide/Code/directives/confirmPopup.html"),
        scope: {
            options: "="
        },
        controller: ["$scope",
            function ($scope) {
                        
            $scope.confirm = function () {
                $scope.options.isConfirmed = true;
                $scope.options.isOpen = false;
                if($scope.options.confirmCallBack){
                    $scope.options.confirmCallBack();
                }
            };

            $scope.notConfirm = function () {
                $scope.options.isConfirmed = false;
                $scope.options.isOpen = false;

                if($scope.options.notConfirmCallBack){
                    $scope.options.notConfirmCallBack();
                }
            };
        }]
    };
});


//Example using this directive with a sharedDataService :  <confirm-popup options="globalSharedData.confirmPopup"></confirm-popup>

		// var sharedData = {     
			// confirmPopupInitial:null,		
            // confirmPopup:null
        // };
		// var confirmPopupInitial = {
            // isOpen: false,
            // isConfirmed: false,
            // confirmCallBack: null,
            // notConfirmCallBack: null,
            // classWrapper: "apf-wrapper",
            // classContent: "apf-popup apf-popup-warning",
            // yesText: "Yes",
            // noText: "No",
            // messageText: "Confirm?"
        // };
        // sharedData.confirmPopupInitial = confirmPopupInitial
        // sharedData.confirmPopup = angular.copy(confirmPopupInitial);
		
		// var openConfirmPopup = function (confirmCallBack, messageText, notConfirmCallBack, classContent, classWrapper) {

            // sharedData.confirmPopup = angular.copy(sharedData.confirmPopupInitial);

            // if (confirmCallBack) {
                // sharedData.confirmPopup.confirmCallBack = confirmCallBack;
            // }

            // if (messageText) {
                // sharedData.confirmPopup.messageText = messageText;
            // }

            // if (notConfirmCallBack) {
                // sharedData.confirmPopup.notConfirmCallBack = notConfirmCallBack;
            // }

            // if (classContent) {
                // sharedData.confirmPopup.classContent = classContent;
            // }
            // if (classWrapper) {
                // sharedData.confirmPopup.classWrapper = classWrapper;
            // }

            // sharedData.confirmPopup.isConfirmed = false;
            // sharedData.confirmPopup.isOpen = true;
        // };
