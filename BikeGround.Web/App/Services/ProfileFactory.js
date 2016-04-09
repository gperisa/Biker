(function () {

    var ProfileFactory = function ($http) {

        var factory = {};

        factory.getProfile = function (ProfileID) {
            return $http.get('http://localhost:3668/api/profile/' + ProfileID);
        };
        
        factory.postProfile = function (Profile) {
            return $http.post('http://localhost:3668/api/profile/', Profile);
        };
        
        factory.putProfile = function (Profile) {
            return $http.put('http://localhost:3668/api/profile/' + Profile.ID, Profile);
        };
                
        return factory;
    };

    ProfileFactory.$inject = ['$http'];

    angular.module('ProfileModule').factory('ProfileFactory', ProfileFactory);

}());