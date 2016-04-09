(function () {
    'use strict';
    var GlobalController = function ($scope, $http, $location, ObjectFactory, Notification, ngProgress) {

        $scope.Global = {};
        $scope.ChatText = [];
        $scope.ActiveChat = false;
        $scope.dateOptions = {
            'year-format': "'yy'",
            'starting-day': '1'
        };

        $scope.Notifications = [];
        $scope.Notifications.push({ 'ID' : '0', 'Name' : '-', 'Title' : '-', 'Time' : '' });
        $scope.headerbar = false;

        $.connection.hub.url = "http://localhost:3668/signalr";
        var chat = $.connection.notificationHub;

        $scope.safeApply = function (fn) {
            var phase = this.$root.$$phase;

            if (phase === '$apply' || phase === '$digest') {
                if (fn && (typeof (fn) === 'function')) {
                    fn();
                }
            } else {
                this.$apply(fn);
            }
        };

        function outputMessage(user, text) {
            $scope.safeApply(function () {
                $scope.ChatText.unshift({ 'user': user, 'text': text });
            });
        }
        
        chat.client.pushMessage = function (name, message) {
            outputMessage(name, message);
        };

        chat.client.pushMessage2 = function (name, obj) {
            $scope.safeApply(function () {
                $scope.Notifications.push({ 'ID': obj.notificationType, 'Name' : name,  'Title': obj.notificationMessage, 'Time' : obj.notificationsDate });
            });  
        };

        function resolveUser() {
            var user = localStorage.getItem('userName');

            if (user) {
                $scope.IsUserName = user;
            } else {
                $scope.IsUserName = 'Profile';
            }
        }

        function IsLoged() {

            $scope.Loged = (!(localStorage.getItem('acc_token') === undefined || localStorage.getItem('acc_token') === null) &&
                            !(localStorage.getItem('userName') === undefined || localStorage.getItem('userName') === null));

            return $scope.Loged;
        }
        
        function userSignIn(user, pass) {
            return $http({
                method: 'POST',
                url: 'http://localhost:3668/token',
                data: 'grant_type=password&username=' + user + '&password=' + pass,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }

        function userRegister(Register) {
            return $http.post('http://localhost:3668/api/login', Register);
            //return $http({
            //    method: 'POST',
            //    url: 'http://localhost:3668/api/login',
            //    data: Register
            //});
        }
        // http://victorbjelkholm.github.io/ngProgress/#demo
        function init() {
            if (!new IsLoged()) {
                return;
            }

            ngProgress.start();
            resolveUser();

            ObjectFactory.getSpecial('starter', 'init').success(function (GlobalData) {
                if (!GlobalData || GlobalData === {}) {
                     return;
                }
                $scope.Global = GlobalData;
                $.connection.hub.start().done(function () {           
                    if ($scope.IsUserName !== 'Profile' && $scope.Loged && GlobalData.ChatActivity) {
                        chat.server.subscribe($scope.IsUserName).done(function () {
                            $scope.safeApply(function () {
                                $scope.ActiveChat = true;
                            });
                        });
                    }
                });

                ngProgress.complete();
            }).error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
                ngProgress.reset();
            });
        }

        init();

        $scope.LogOut = function LogOut() {
            localStorage.removeItem('acc_token');
            localStorage.removeItem('userName');

            $scope.Global = {};
            $scope.Loged = false;
            $location.path("/Login");
        };

        $scope.LoginFormValidate = function (isValid, Login) {
            if (isValid) {
                userSignIn(Login.UserName, Login.Password).success(function (data) {
                    localStorage.setItem('userName', Login.UserName);
                    localStorage.setItem('acc_token', data.access_token);

                    init();
                    resolveUser();
                    $location.path('/Start');
                }).error(function (data, status, headers, config) {
                    Notification.error({ message: 'Error: ' + status, delay: 2000 });
                });
            }
        };

        $scope.RegisterFormValidate = function (isValid, Register) {
            if (isValid) {
                userRegister(Register).success(function (data) {
                    Notification.error({ message: 'Successul registration', delay: 2000 });
                    $location.path('/Login');
                }).error(function (data, status, headers, config) {
                    Notification.error({ message: 'Error: ' + status, delay: 2000 });
                });
            }
        };

        /********************************** Chat **********************************/
        
        $scope.subscribe = function (user) {
            if ($scope.IsUserName !== 'Profile' && $scope.Loged) {
                chat.server.subscribe(user);
            }
        };

        $scope.usubscribe = function (user) {
            if ($scope.IsUserName !== 'Profile' && $scope.Loged) {
                chat.server.subscribe(user);
            }
        };

        $scope.send = function (message, chatUser) {
            if (message && chatUser) {
                chat.server.send(chatUser, $scope.IsUserName, message);
                outputMessage($scope.IsUserName, message);
            }
        };

        //$(document).ready(function () {
        //    var chat = $.connection.NotificationHub;
        //    $.connection.hub.start();
        //    var user = $('#UsernameID').val();
        //    $.connection.hub.start().done(function () {
        //        $('#SubmitID').on('click', function () {
        //            var grupa = $('#GrupaID').val();
        //            var text = $('#ChatID').val();

        //            if (user == undefined || user == null) {
        //                user = $('#UsernameID').val();
        //            }

        //            if (text != undefined && text != '') {
        //                chat.server.send(grupa, user, text);
        //                $('#ChatID').val('').focus();

        //                outputMessage(user, text);
        //            }
        //        });
        //    });
        //});
    };

    GlobalController.$inject = ['$scope', '$http', '$location', 'ObjectFactory', 'Notification', 'ngProgress'];

    angular.module('GlobalModule').controller('GlobalController', GlobalController);

}());