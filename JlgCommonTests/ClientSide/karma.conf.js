module.exports = function(config) {
    config.set({
        basePath: "../../",

        files: [
            "Web/scripts/dependencies/angular.js",
            "Web/scripts/dependencies/**/*.js",                        
            "Web/scripts/apfModules.js",
            "Web/scripts/localRoutesTexts.js",
            "Web/scripts/apfRoutes.js",
            "Web/scripts/app/**/*.js",
            "Tests/Client/dependencies/angular/angular-mocks.js",
            "Tests/Client/tests/**/*.js",

            //Templates
            "Web/scripts/app/**/*.html"
        ],

        exclude: [
            "Tests/Client/tests/karma.conf.js"
        ],
        preprocessors: {
            "Web/scripts/app/**/*.html": ["ng-html2js"]
        },
        ngHtml2JsPreprocessor: {
            stripPrefix: 'Web/',
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
