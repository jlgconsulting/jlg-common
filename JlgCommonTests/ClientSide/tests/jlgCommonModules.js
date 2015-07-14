console.info('Starting tests for Jlg.Common ClientSide');

angular.module("jlgCommon", ["jlg.common.services", "jlg.common.directives"]);

    angular.module("jlg.common.services", []);
    angular.module("jlg.common.directives", []);  
    

    'use strict';
    beforeEach(function () {
        //this will be executed globally!
        window.urlGetter = function (url) {
            return url;
        };

        module(function ($provide) {

            var confirmPopupInitial = {
                isOpen: false,
                isConfirmed: false,
                originUniqueToken: null,
                objectForConfirmation: null,
                text: null,
                className: "apf-popup-warning",
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

            $provide.value('sharedDataAndPopupSrv', {
                sharedData: sharedData,
                resetConfirmPopup: resetConfirmPopup
            });
        });
        
       
    });

   
  