(function () {

    var ConnectController = function ($scope, ObjectFactory, Notification) {

        $scope.Connections = [];

        $scope.ConnectFormValidate = function (isValid, Connect) {
            if (isValid) {
                ObjectFactory.postObject(Connect, 'Connect')
                .success(function (ConnectData) {
                    console.log(ConnectData);
                    $scope.Connections = ConnectData;

                    Notification.success({ message: 'Ok!', delay: 2000 });
                })
                .error(function (data, status, headers, config) {
                    Notification.error({ message: 'Error: ' + status, delay: 2000 });
                });
            }
        }
    };

    ConnectController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('ConnectModule').controller('ConnectController', ConnectController);

}());