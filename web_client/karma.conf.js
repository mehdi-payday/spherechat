//jshint strict: false
module.exports = function(config) {
  config.set({

    basePath: './app',

    files: ['bower_components/angular/angular.js',
		'bower_components/angular-ui-router/release/angular-ui-router.js',
		'bower_components/betsol-load-stylesheet/dist/betsol-load-stylesheet.js',
		'bower_components/betsol-ng-ui-router-styles/dist/scripts/betsol-ng-ui-router-styles.js',
		'bower_components/angular-animate/angular-animate.js',
		'bower_components/angular-resource/angular-resource.js',
		'bower_components/ngstorage/ngStorage.js',
		'bower_components/angular-mocks/angular-mocks.js',
		            
		'app.js',
		            
		'website/main/main.js',
		'website/404/404.js',
		'website/502/502.js',
		'website/about/about.js',
		'website/comingsoon/comingsoon.js',
		'website/downloads/downloads.js',
		'website/faq/faq.js',
		'website/login/login.js',
		'website/resetpassword/resetpassword.js',
		'website/signup/signup.js',
		'webclient/webclient.js',
		
		'tests/api.spec.js'
    ],

    autoWatch: true,

    frameworks: ['jasmine'],

    browsers: ['Chrome'],

    plugins: [
      'karma-chrome-launcher',
      'karma-jasmine',
      'karma-junit-reporter'
    ],

    junitReporter: {
      outputFile: 'test_out/unit.xml',
      suite: 'unit'
    }

  });
};
