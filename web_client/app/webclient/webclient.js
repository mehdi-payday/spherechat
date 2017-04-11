'use strict';

angular.module('myApp.webclient', ['ngRoute'])

.controller('WebclientCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);