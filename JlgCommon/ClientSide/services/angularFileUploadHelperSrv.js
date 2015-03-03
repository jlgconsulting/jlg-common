var sharedModule = angular.module("shared");
sharedModule.factory('angularFileUploadHelperSrv',
    ["$upload",
    function ($upload) {

    return {
        getUploadProgress: function(evt) {
            return parseInt(100.0 * evt.loaded / evt.total);
        },
        getUploadPromise: function(files, url, multiple) {
            if (!files || !files[0]) {
                return;
            }

            var contentToUpload = files[0];
            if (multiple) {
                contentToUpload = files;
            }

            var uploadPromise =
                $upload.upload({
                    url: url,
                    file: contentToUpload
                });

            uploadPromise.error(function(response) {
                console.log($scope.translatedText.failed + ": " + response);
                toastr.error($scope.translatedText.failed);
            });

            return uploadPromise;
        }
    };
}]);
