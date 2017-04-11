'use strict';

angular.module('myApp.signup', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/signup', {
    templateUrl: 'website/signup/signup.html',
    controller: 'SignupCtrl'
  });
}])

.controller('SignupCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);