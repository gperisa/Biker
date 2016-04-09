(function () {

    var MessagesController = function ($scope, ObjectFactory, Notification) {

        $scope.Messages = {};

        function init() {

            $scope.Messages = ObjectFactory.getObject(MessagesID, 'Messages')
            .success(function (MessagesData) {
                $scope.Messages = MessagesData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        init();

        $scope.MessagesFormValidate = function (isValid, Messages) {

            if (isValid) {
                if (Messages.ID === 0 || Messages.ID === undefined) {
                    ObjectFactory.postObject(Messages, 'Messages')
                    .success(function (ID) {

                        $scope.Messages.ID = ID;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else if (Messages.ID > 0) {
                    ObjectFactory.putObject(Messages, 'Messages')
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

    MessagesController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('MessagesModule').controller('MessagesController', MessagesController);

}());