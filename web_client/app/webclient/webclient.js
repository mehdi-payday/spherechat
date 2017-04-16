'use strict';

angular.module('myApp.webclient', ['session', 'auth'])

.controller('WebclientCtrl', ['$rootScope', 'session', 'auth', function($rootScope, session, auth) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
	
	
}]);