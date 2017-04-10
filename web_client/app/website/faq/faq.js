'use strict';

angular.module('myApp.faq', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/faq', {
    templateUrl: 'website/faq/faq.html',
    controller: 'FaqCtrl'
  });
}])

.controller('FaqCtrl', [function() {

}]);