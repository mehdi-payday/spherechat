'use strict';

angular.module('myApp.502', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/502', {
    templateUrl: 'website/502/502.html',
    controller: '502Ctrl'
  });
}])

.controller('502Ctrl', [function() {

}]);