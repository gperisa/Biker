(function () {

    var CommentController = function ($scope, ObjectFactory, Notification) {

        $scope.Comment = {};
        $scope.Comments = {};

        function init() {

            if ($scope.idx > 0) {

                console.log($scope.idx + ' -> ' + Date.now());

                ObjectFactory.getObject($scope.idx, 'Comments')
                .success(function (CommentData) {
                    $scope.Comments = CommentData;
                })
                .error(function (data, status, headers, config) {
                    Notification.error({ message: 'Error: ' + status, delay: 2000 });
                });
            }
        }

        init();

        $scope.refresh = function () {
            init();
        }

        $scope.setComment = function (ID) {

            if (ID === 0) {
                $scope.Comment = {};
                return;
            }

            for (var i in $scope.Comments) {
                if ($scope.Comments[i].ID === ID) {
                    $scope.Comment = $scope.Comments[i];
                    break;
                }
            }
        }

        $scope.CommentFormValidate = function (isValid, Comment) {

            if (isValid) {

                Comment.PostID = $scope.idx;
                Comment.CommentID = 1;

                if (Comment.ID === 0 || Comment.ID === undefined) {
                    ObjectFactory.postObject(Comment, 'Comment')
                    .success(function (ID) {

                        $scope.Comment.ID = ID;
                        $scope.Comments.push(Comment);

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else if (Comment.ID > 0) {
                    ObjectFactory.putObject(Comment, 'Comment')
                    .success(function () {
                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
            }
        };
    };

    CommentController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('CommentModule').controller('CommentController', CommentController);

}());