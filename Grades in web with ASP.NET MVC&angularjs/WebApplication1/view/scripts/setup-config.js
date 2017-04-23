define([], function () {//'app2'
    'use strict';
	function setupConfig($routeProvider) {
	    $routeProvider
    .when('/', {
        templateUrl: '../template/home.html',
        controller: 'loginController  as loginCtrl'
    }).when('/about', {
        templateUrl: '../template/about.html',
        controller: 'loginController  as loginCtrl'
    }).when('/register', {
        templateUrl: '../template/register.html',
        controller: 'loginController  as loginCtrl'
    }).when('/login', {
        templateUrl: '../template/login.html',
        controller: 'loginController  as loginCtrl'
    }).when('/courses', {
        templateUrl: '../template/courses.html',
        controller: 'courseController as courseCtrl'
    }).when('/draw', {
        templateUrl: '../template/drawTemplete.html',
        controller: 'drawController as drawCtrl'
    });
    }
    return ["$routeProvider",setupConfig];
});
