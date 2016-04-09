(function () {

    var BlogController = function ($scope, ObjectFactory, Notification) {

        $scope.Blog = {};

        $scope.init = function () {

            ObjectFactory.getSingle('Blog')
            .success(function (BlogData) {
                $scope.Blog = BlogData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        $scope.init();

        $scope.BlogFormValidate = function (isValid, Blog) {
            // check to make sure the form is completely valid
            if (isValid) {

                if (Blog.ID === 0 || Blog.ID === undefined) {
                    ObjectFactory.postObject(Blog, 'Blog')
                    .success(function (ID) {

                        $scope.Blog.ID = ID;
                        $scope.Global.Blog = $scope.Blog;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else {
                    ObjectFactory.putObject(Blog, 'Blog')
                    .success(function () {
                        $scope.Global.Blog = $scope.Blog;
                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
            }
        };
    };

    BlogController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('BlogModule').controller('BlogController', BlogController);

}());
