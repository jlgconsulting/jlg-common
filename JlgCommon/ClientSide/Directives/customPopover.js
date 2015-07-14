var directivesModule = angular.module("jlg.common.directives");
directivesModule.directive('customPopover', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
          $(el).popover({
                trigger: 'click',
                html: true,
                content: attrs.popoverHtml,
                placement: attrs.popoverPlacement,
                //container: "body"
            });
        }
    };
});

   