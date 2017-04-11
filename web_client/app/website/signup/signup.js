'use strict';

angular.module('myApp.signup', ['ngRoute'])

.controller('SignupCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);