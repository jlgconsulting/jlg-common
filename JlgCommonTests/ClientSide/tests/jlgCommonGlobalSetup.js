console.info('Starting tests for Jlg.Common ClientSide');

angular.module("jlgCommon", ["jlg.common"]);

    angular.module("jlg.common", []);      

    'use strict';
    beforeEach(function () {
        //this will be executed globally!
        window.serverAppPath = function (url) {
            return url;
        };

        module(function ($provide) {

            var confirmPopupInitial = {
                isOpen: false,
                isConfirmed: false,
                originUniqueToken: null,
                objectForConfirmation: null,
                messageText: null,
                classWrapper: "apf-popup-warning",
                classContent: "apf-popup-warning",
                tokens: {
                    deleteCategory: "deleteCategory",
                    deleteUser: "deleteUser"
                }
            }

            var sharedData = {
                confirmPopup: angular.copy(confirmPopupInitial),
                sectionTitle: {
                    show: false,
                    title: null,
                    goBackCallback: null
                }
            }     

            var resetConfirmPopup = function () {

                sharedData.confirmPopup = angular.copy(confirmPopupInitial);
            };

            $provide.value('globalSharedService', {
                sharedData: sharedData,
                resetConfirmPopup: resetConfirmPopup
            });
        });
        
       
    });

   
  