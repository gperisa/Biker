(function () {

    var GlobalFactory = function ($http, Notification) {

        var factory = {};

        factory.getData = function (ObjName) {
            return $http.get('http://localhost:3668/api/starter/init');
        };

        return factory;
    };

    GlobalFactory.$inject = ['$http'];

    //angular.module('GlobalModule').factory('GlobalFactory', GlobalFactory);
    angular.module('GlobalModule').factory('GlobalFactory', GlobalFactory);

}());