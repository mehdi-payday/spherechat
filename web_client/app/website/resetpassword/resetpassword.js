'use strict';

angular.module('myApp.resetpassword', [])

.controller('ResetpasswordCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);