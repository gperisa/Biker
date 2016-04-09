(function () {

    var PostFactory = function ($http) {

        var factory = {};

        factory.getPost = function (PostID) {
            return $http.get('http://localhost:3668/api/post/' + PostID);
        };

        factory.postPost = function (Post) {
            return $http.post('http://localhost:3668/api/post/', Post);
        };

        factory.putPost = function (Post) {
            return $http.put('http://localhost:3668/api/post/' + Post.ID, Post);
        };

        return factory;
    };

    PostFactory.$inject = ['$http'];

    angular.module('PostModule').factory('PostFactory', PostFactory);

}());