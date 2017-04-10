'use strict';

angular.module('myApp.comingsoon', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/comingsoon', {
    templateUrl: 'website/comingsoon/comingsoon.html',
    controller: 'ComingsoonCtrl'
  });
}])

.controller('ComingsoonCtrl', [function() {

}]);