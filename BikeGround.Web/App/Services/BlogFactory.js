(function () {

    var BlogFactory = function ($http) {

        var factory = {};

        factory.getBlog = function (BlogID) {
            return $http.get('http://localhost:3668/api/blog/' + BlogID);
        };

        factory.postBlog = function (Blog) {
            return $http.post('http://localhost:3668/api/blog/', Blog);
        };

        factory.putBlog = function (Blog) {
            return $http.put('http://localhost:3668/api/blog/' + Blog.ID, Blog);
        };

        return factory;
    };

    BlogFactory.$inject = ['$http'];

    angular.module('BlogModule').factory('BlogFactory', BlogFactory);

}());