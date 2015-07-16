module.exports = function(config) {
    config.set({
        basePath: "../../",

        files: [
            
            "JlgCommonTests/ClientSide/dependencies/angular/angular.js",
            "JlgCommonTests/ClientSide/dependencies/angular/angular-mocks.js",
            "JlgCommonTests/ClientSide/tests/jlgCommonGlobalSetup.js",
            "JlgCommonTests/ClientSide/tests/**/*.js",

            "JlgCommon/ClientSide/**/*.js",            
            "JlgCommon/ClientSide/**/*.html"
        ],

        exclude: [
            "JlgCommonTests/ClientSide/karma.conf.js"
        ],
        preprocessors: {
            "JlgCommon/ClientSide/**/*.html": ["ng-html2js"]
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
