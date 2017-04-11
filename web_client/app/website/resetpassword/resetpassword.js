'use strict';

angular.module('myApp.resetpassword', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/resetpassword', {
    templateUrl: 'website/resetpassword/resetpassword.html',
    controller: 'ResetpasswordCtrl'
  });
}])

.controller('ResetpasswordCtrl', ['$rootScope', function($rootScope) {
	$rootScope.hideNavbar = true;
	$rootScope.hideFooter = true;
}]);