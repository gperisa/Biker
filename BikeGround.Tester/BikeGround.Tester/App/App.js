(function () {

    // ovo je cijela aplikacija
    var app = angular.module('BikeGround', ['ngRoute']);

    // ovdje se definiraju rute
    app.config(function ($routeProvider, $httpProvider, $locationProvider) {

        $routeProvider
        .when('/Profile', {
            //controller: 'ProfileController',
            templateUrl: '/Profile/Index'
        })
        .when('/Vila', {
            //controller: 'ProfileController',
            templateUrl: '/Vila/Index'
        })
        .when('/Login', {
            //controller: 'ProfileController',
            templateUrl: '/Login/Index'
        })
        .otherwise({ redirectTo: '/' });

        //$locationProvider.html5Mode(true);
    });

    /********************************************************************/
    // register the interceptor as a service
    // app.factory('myHttpInterceptor', function ($q) {
    app.factory('myHttpInterceptor', function ($q, $location) {
        return {
            'request': function (config) {

                //config.headers = config.headers || {};

                if (app.token != undefined) {
                    config.headers['Authorization'] = 'Bearer ' + app.token;
                }

                // do something on success
                return config;
            },
            'requestError': function (rejection) {

                // do something on error
                //if (canRecover(rejection)) {
                //    return responseOrNewPromise
                //}
                return $q.reject(rejection);
            },
            'response': function (response) {
                // do something on success
                return response;
            },
            'responseError': function (rejection) {
                if (rejection.status === 401) {
                    $location.path('/Login');
                    rejection.data = { stauts: 401, description: 'unauthorized' }
                    return rejection.data;
                }

                // do something on error
                //if (canRecover(rejection)) {
                //    return responseOrNewPromise
                //}
                return $q.reject(rejection);
            }
        };
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('myHttpInterceptor');
    });
    
    //app.run(function ($rootScope, $location, $timeout) {

    //    $rootScope.$on("$routeChangeError", function (event, current, previous, rejection) {

    //        if ($rootScope.requestError === 401)
    //        {
    //            console.log($rootScope.requestError + ' ' + Date.now());
    //            $rootScope.requestError = '';
    //        }
    //    });

    //    $rootScope.$on('loginRequired', function () {
    //        console.log('Stari login je required ' + Date.now());
    //        $location.path('/login');
    //    });
    //});

    /********************************************************************/

    app.controller('mainController', function ($scope, $http, $location) {

        $scope.exclude = "/^[^<>#}{$%&+-,:;:]+$/";
        $scope.unicode = "[\pL\pM\p{Nl}]";
        $scope.regSingle = "/^\\w+$/";
        $scope.regMultiple = "/^\\w+( \\w+)*$/";

        // function to submit the form after all validation has occurred
        $scope.submitForm = function (isValid) {

            // check to make sure the form is completely valid
            if (isValid) {

                var o = $scope.user;

                loginShit(o.email, o.pass).success(function (data) {
                    
                    //$scope.token = data['access_token'];
                    app.token = data['access_token'];
                    console.log('Success: ' + app.token);
                    //getUrl().success(function () {
                    //    console.log('Uspješno pozvano');
                    //})
                    //.error(function () {
                    //    console.log('Neuspješno pozvano');
                    //})
                })
                .error(function (data, status, headers, config) {
                    alert('greška fucking greška');
                });


            }
            else {
                alert('Forma nije validna');
            }
        };

        function loginShit(user, pass) {
            console.log(user + ' ' + pass);
            return $http({
                method: 'POST',
                url: 'http://localhost:3668/token',
                data: 'grant_type=password&username=' + user + '&password=' + pass,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            })

            //headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }

        //function getUrl() {
        //    return $http({
        //        method: 'GET',
        //        url: 'http://localhost:5732/Vila',
        //        headers: { 'Content-Type': 'application/x-www-form-urlencoded', Authorization: 'Bearer ' + app.token }
        //    })
        //}
    });

}());