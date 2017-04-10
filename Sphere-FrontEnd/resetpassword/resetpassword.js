'use strict';

angular.module('myApp.resetpassword', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/resetpassword', {
    templateUrl: 'resetpassword/resetpassword.html',
    controller: 'ResetpasswordCtrl'
  });
}])

.controller('ResetpasswordCtrl', [function() {

}]);