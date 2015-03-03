var directivesModule = angular.module("directives");
directivesModule.directive("uploadFiles", function () {
    return {        
        restrict: "E",
        templateUrl: window.urlGetter("scripts/app/_directives/shared/uploadFiles.html"),
        scope: {
            textDescription: "=",
            urlDestination: "=",
            serverUploadResponse: "=",
            uploadProgress: "=",
            multiple: "@"
        },
        controller: ["$scope", "$upload", "apfSharedDataAndPopupSrv",
            function ($scope, $upload, apfSharedDataAndPopupSrv) {

            $scope.apfSharedData = apfSharedDataAndPopupSrv.sharedData;
            $scope.$watch("apfSharedData.loggedInContext", function (newValue) {
                if (newValue) {
                    $scope.translatedText = newValue.translatedText;
                }
            });

            $scope.onFilesSelect = function (files) {
                if (!files || !files[0]) {
                    return;
                }

                var contentToUpload = files[0];
                if ($scope.multiple) {
                    contentToUpload = files;
                }                
                $upload.upload({
                    url: $scope.urlDestination,
                    file: contentToUpload
                }).progress(function (evt) {
                    $scope.uploadProgress = parseInt(100.0 * evt.loaded / evt.total);
                }).success(function (response, status, headers, config) {
                    $scope.serverUploadResponse = response;
                    if (config.file.length==1) {
                        $scope.uploadedFileName=config.file[0].name;
                    }
                }).error(function (response) {
                    console.log($scope.translatedText.failed + ": " + response);
                    toastr.error($scope.translatedText.failed);
                });
            };
        }]
    };
});

