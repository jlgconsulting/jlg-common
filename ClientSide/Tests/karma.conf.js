module.exports = function(config) {
    config.set({
        basePath: "../../",

        files: [
            
            "ClientSide/Tests/dependencies/angular/angular.js",
            "ClientSide/Tests/dependencies/angular/angular-mocks.js",
            "ClientSide/Tests/tests/jlgCommonGlobalSetup.js",
            "ClientSide/Tests/tests/**/*.js",

            "ClientSide/Code/**/*.js",            
            "ClientSide/Code/**/*.html"
        ],

        exclude: [
            "ClientSide/Tests/karma.conf.js"
        ],
        preprocessors: {
            "ClientSide/Code/**/*.html": ["ng-html2js"]
        },
        ngHtml2JsPreprocessor: {
            stripPrefix: 'JlgCommon/',
            moduleName: 'alltemplates'
        },

        autoWatch: true,

        frameworks: ["jasmine"],

        browsers: ["Chrome"],

        plugins: [
            "karma-junit-reporter",
            "karma-chrome-launcher",
            "karma-firefox-launcher",
            "karma-jasmine",
            "karma-ng-html2js-preprocessor"
        ],

        junitReporter: {
            outputFile: "test_out/unit.xml",
            suite: "unit"
        }
    });
}
