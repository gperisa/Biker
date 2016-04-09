(function () {

    // ovdje definiramo module
    angular.module('ProfileModule', ['ui-notification']);
    angular.module('BlogModule', ['ui-notification']);
    angular.module('PostModule', ['ui-notification', 'textAngular']);
    angular.module('WallModule', ['ui-notification']);
    angular.module('SettingsModule', ['ui-notification']);
    angular.module('TripModule', ['ui-notification']);
    angular.module('ObjectModule', ['ui-notification']);
    angular.module('GlobalModule', ['ui-notification']);
    angular.module('CommentModule', ['ui-notification']);
    angular.module('MailboxModule', ['ui-notification']);
    angular.module('ConnectModule', ['ui-notification']);
    angular.module('AnalyticsModule', ['ui-notification']);
    angular.module('NotificationModule', ['ui-notification']);
    angular.module('MessagesModule', ['ui-notification']);

    // ovo je cijela aplikacija
    var app = angular.module('BikeGround', ['ngRoute', 'ui.bootstrap', 'ngProgress', 'ngAnimate', 'ngResource', 'toggle-switch', 'ProfileModule', 'BlogModule', 'PostModule', 'WallModule', 'SettingsModule', 'TripModule', 'MailboxModule', 'CommentModule', 'ObjectModule', 'GlobalModule', 'ConnectModule', 'AnalyticsModule', 'NotificationModule', 'MessagesModule']);

    // ovdje se definiraju rute
    app.config(function ($routeProvider, $httpProvider) {

        $routeProvider
        .when('/Profile', {
            //controller: 'ProfileController',
            templateUrl: '/Profile/Index'
        })
        .when('/Trip', {
            //controller: 'ProfileController',
            templateUrl: '/Trip/Index'
        })
        .when('/Blog', {
            //controller: 'BlogController',
            templateUrl: '/Blog/Index'
        })
        .when('/Post', {
            //controller: 'BlogController',
            templateUrl: '/Post/Index'
        })
        .when('/Wall', {
            //controller: 'BlogController',
            templateUrl: '/Wall/Index'
        })
        .when('/Mailbox', {
                //controller: 'BlogController',
                templateUrl: '/Mailbox/Index'
            })
        .when('/Comment', {
            //controller: 'BlogController',
            templateUrl: '/Comment/Index'
        })
        .when('/Login', {
            //controller: 'BlogController',
            templateUrl: '/W_Login/Index'
        })
        .when('/Settings', {
            //controller: 'BlogController',
            templateUrl: '/W_Settings/Index'
        })
        .when('/Connect', {
            //controller: 'BlogController',
            templateUrl: '/W_Connect/Index'
        })
        .when('/Analytics', {
            //controller: 'BlogController',
            templateUrl: '/Analytics/Index'
        })
        .when('/Notification', {
            //controller: 'BlogController',
            templateUrl: '/Notification/Index'
        })
        .when('/Messages', {
            //controller: 'BlogController',
            templateUrl: '/Messages/Index'
        })
        .otherwise({
            redirectTo: '/Start',
            templateUrl: '/W_Start/Index'
        });
    });

    app.factory('customHttpInterceptor', function ($q, $location, $templateCache) {
        return {
            'request': function (config) {
                
                // obavezno učitaj template
                // $templateCache.remove('/Start/Index');

                // IE fix za obavezno učitavanje template-a jer $templateCache.remove('/Start/Index') ne radi
                if (config.method === 'GET' && config.url === '/Start/Index') {
                    var sep = config.url.indexOf('?') === -1 ? '?' : '&';
                    config.url = config.url + sep + 'csx=' + new Date().getTime();
                }

                if (localStorage.getItem('acc_token') != undefined &&
                    config.url.indexOf('token') === -1 &&
                    config.url.indexOf('Login') === -1
                    ) {
                    config.headers['Authorization'] = 'Bearer ' + localStorage.getItem('acc_token');
                }
                
                return config;
            },
            'requestError': function (rejection) {
                return $q.reject(rejection);
            },
            'response': function (response) {
                return response;
            },
            'responseError': function (rejection) {
                if (rejection.status === 401) {
                    $location.path('/Login');
                    rejection.data = { stauts: 401, description: 'unauthorized' }
                    return rejection.data;
                }

                return $q.reject(rejection);
            }
        };
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('customHttpInterceptor');
    });

    app.run(function($rootScope, ngProgress) {
        $rootScope.$on('$routeChangeStart', function() {
            ngProgress.start();
        });

        $rootScope.$on('$routeChangeSuccess', function() {
            ngProgress.complete();
        });
        // Do the same with $routeChangeError
    });

    //app.controller('mainController', function ($scope, $rootScope, $http, $location, $route, $templateCache) {

    //    $scope.exclude = "/^[^<>#}{$%&+-,:;:]+$/";
    //    $scope.unicode = "[\pL\pM\p{Nl}]";
    //    $scope.regSingle = "/^\\w+$/";
    //    $scope.regMultiple = "/^\\w+( \\w+)*$/";

    //    // function to submit the form after all validation has occurred

    //});

    app.directive('datedirective', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ngModel) {

                function fromUser(text) {
                    return parseFromDate(text, attr.dateformat);
                }

                function toUser(text) {
                    return parseToDate(text, attr.dateformat);
                }

                ngModel.$parsers.push(fromUser);
                ngModel.$formatters.push(toUser);
            }
        };
    });

    app.directive('ngprofile', function () {
        return {
            restrict: 'E',
            transclude: false,
            //controller: 'ProfileController',
            templateUrl: '/Profile/Index'
        };
    });

    app.directive('ngblog', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Blog/Index'
        };
    });

    app.directive('ngpost', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Post/Index'
        };
    });

    app.directive('ngwall', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Wall/Index'
        };
    });

    app.directive('ngtrip', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Trip/Index'
        };
    });

    app.directive('ngmailbox', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Mailbox/Index'
        };
    });

    app.directive('ngcomment', function () {
        return {
            restrict: 'E',
            transclude: false,
            scope: {
                idx: '@'
            },
            templateUrl: '/Comment/Index'
        };
    });

    app.directive('nganalytics', function () {
        return {
            restrict: 'E',
            transclude: false,
            templateUrl: '/Analytics/Index'
        };
    });

    function pad(num, size) {
        var s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }

    function parseFromDate(entry, format) {

        // new Date(dd, MM, yyyy)

        if (format == undefined) {
            return entry;
        }

        var ret = entry;

        if (format === "dd.MM.yyyy") {
            var dateParts = entry.split(".");

            if (dateParts[2] != undefined && dateParts[1] != undefined && dateParts[0] != undefined) {
                return new Date(dateParts[2] + '-' + pad(dateParts[1], 2) + '-' + pad(dateParts[0], 2));
            }
        }
        else if (format === "yyyy-MM-dd") {
            var dateParts = entry.split("-");

            if (dateParts[2] != undefined && dateParts[1] != undefined && dateParts[0] != undefined) {
                return new Date(dateParts[0] + '-' + pad(dateParts[1], 2) + '-' + pad(dateParts[2], 2));
            }
        }

        return ret;
    }

    function parseToDate(entry, format) {

        // new Date(dd, MM, yyyy)

        if (entry == undefined) {
            return entry;
        }

        var d = new Date(entry);

        if (d == undefined) {
            return entry;
        }

        var ret = entry;

        if (format === "dd.MM.yyyy") {
            return pad(d.getDate(), 2) + '.' + pad((d.getMonth() + 1), 2) + '.' + d.getFullYear();
        }
        else if (format === "yyyy-MM-dd") {
            return d.getFullYear() + '-' + pad((d.getMonth() + 1), 2) + '-' + pad(d.getDate(), 2);
        }

        return ret;
    }

}());