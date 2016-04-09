
(function () {

    var MailboxController = function ($scope, ObjectFactory, Notification) {

        $scope.Mailbox = {};
        $scope.Mailboxes = {};

        function init() {

            $scope.Mailboxes = ObjectFactory.getMultiple('Mailbox')
            .success(function (MailboxData) {
                $scope.Mailboxes = MailboxData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }
        
        init();

        $scope.getSentMail = function getSentMail() {

            $scope.Mailboxes = ObjectFactory.getMultiple('Mailbox/Sent')
            .success(function (MailboxData) {
                $scope.Mailboxes = MailboxData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        $scope.refresh = function () {
            init();
        }

        $scope.setMailbox = function (ID) {

            if (ID === 0) {
                $scope.Mailbox = {};
                return;
            }

            for (var i in $scope.Mailboxes) {
                if ($scope.Mailboxes[i].ID === ID) {
                    $scope.Mailbox = $scope.Mailboxes[i];
                    break;
                }
            }
        }

        $scope.MailboxFormValidate = function (isValid, Mailbox) {

            if (isValid) {
                if (Mailbox.ID === 0 || Mailbox.ID === undefined) {
                    ObjectFactory.postObject(Mailbox, 'Mailbox')
                    .success(function (ID) {

                        $scope.Mailbox.ID = ID;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else if (Mailbox.ID > 0) {
                    ObjectFactory.putObject(Mailbox, 'Mailbox')
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

    MailboxController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('MailboxModule').controller('MailboxController', MailboxController);

}());