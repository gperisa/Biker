(function () {

    var PostController = function ($scope, ObjectFactory, Notification) {

        $scope.Post = {};
        $scope.Posts = {}

        function init() {
            $scope.Posts = ObjectFactory.getMultiple('Post')
            .success(function (PostData) {
                $scope.Posts = PostData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + data, delay: 2000 });
            });
        }

        init();

        $scope.refresh = function () {
            init();
        }

        $scope.setPost = function (ID) {

            if (ID === 0) {
                $scope.Post = {};
                return;
            }

            for (var i in $scope.Posts) {
                if ($scope.Posts[i].ID === ID) {
                    $scope.Post = $scope.Posts[i];
                    break;
                }
            }
        }

        $scope.PostFormValidate = function (isValid, Post) {
            // check to make sure the form is completely valid
            if (isValid) {
                if (Post.ID === 0 || Post.ID === undefined) {
                    ObjectFactory.postObject(Post, 'Post')
                    .success(function (ID) {

                        $scope.Post.ID = ID;
                        $scope.Posts.push(Post);
                        //$scope.Global.Posts.push({ ID: ID, Title: Post.Title });

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else {
                    ObjectFactory.putObject(Post, 'Post')
                    .success(function () {
                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
            }
        }
    };

    PostController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('PostModule').controller('PostController', PostController);

}());