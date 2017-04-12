'use strict';

angular.module('myApp.login', ['ngRoute'])

.controller('LoginCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);