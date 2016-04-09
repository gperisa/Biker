(function () {

    var WallFactory = function ($http) {

        var factory = {};

        factory.getWall = function (WallID) {
            return $http.get('http://localhost:3668/api/Starter/' + WallID);
        };
        
        factory.postWall = function (Wall) {
            return $http.post('http://localhost:3668/api/Wall/', Wall);
        };
        
        factory.putWall = function (Wall) {
            return $http.put('http://localhost:3668/api/Wall/' + Wall.ID, Wall);
        };
                
        return factory;
    };

    WallFactory.$inject = ['$http'];

    angular.module('WallModule').factory('WallFactory', WallFactory);

}());