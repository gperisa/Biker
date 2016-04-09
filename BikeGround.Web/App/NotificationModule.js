(function () {

    var NotificationController = function ($scope, ObjectFactory, Notification) {

        $scope.Notification = {};

        function init() {

            $scope.Notification = ObjectFactory.getObject(NotificationID, 'Notification')
            .success(function (NotificationData) {
                $scope.Notification = NotificationData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        init();

        $scope.NotificationFormValidate = function (isValid, Notification) {

            if (isValid) {
                if (Notification.ID === 0 || Notification.ID === undefined) {
                    ObjectFactory.postObject(Notification, 'Notification')
                    .success(function (ID) {

                        $scope.Notification.ID = ID;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else if (Notification.ID > 0) {
                    ObjectFactory.putObject(Notification, 'Notification')
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

    NotificationController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('NotificationModule').controller('NotificationController', NotificationController);

}());