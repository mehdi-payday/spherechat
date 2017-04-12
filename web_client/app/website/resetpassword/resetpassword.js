'use strict';

angular.module('myApp.resetpassword', ['ngRoute'])

.controller('ResetpasswordCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);