(function () {

    var ObjectFactory = function ($http, Notification) {

        var factory = {};

        factory.getSpecial = function (ObjName, keyword) {
            return $http.get('http://localhost:3668/api/' + ObjName + '/' + keyword);
        };

        factory.getSingle = function (ObjName) {
            return $http.get('http://localhost:3668/api/' + ObjName + '/single');
        };

        factory.getMultiple = function (ObjName) {
            return $http.get('http://localhost:3668/api/' + ObjName + '/multiple');
        };

        factory.getObjects = function (ObjName) {
            return $http.get('http://localhost:3668/api/' + ObjName);
        };

        factory.getObject = function (ID, ObjName) {
            return $http.get('http://localhost:3668/api/' + ObjName + '/' + ID);
        };

        factory.postObject = function (Object, ObjName) {
            return $http.post('http://localhost:3668/api/' + ObjName + '/', Object);
        };

        factory.putObject = function (Object, ObjName) {
            return $http.put('http://localhost:3668/api/' + ObjName + '/' + Object.ID, Object);
        };

        return factory;
    };

    ObjectFactory.$inject = ['$http'];

    angular.module('ObjectModule').factory('ObjectFactory', ObjectFactory);

}());