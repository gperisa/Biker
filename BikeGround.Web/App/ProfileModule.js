(function () {

    var ProfileController = function ($scope, ObjectFactory, Notification) {

        $scope.Profile = {};

        $scope.init = function () {

            $scope.Profile = ObjectFactory.getSingle('Profile')
            .success(function (ProfileData) {
                $scope.Profile = ProfileData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        $scope.init();

        $scope.ProfileFormValidate = function (isValid, Profile) {
            // check to make sure the form is completely valid
            if (isValid) {
                if (Profile.ID === 0 || Profile.ID === undefined) {
                    ObjectFactory.postObject(Profile, 'Profile')
                    .success(function (ID) {

                        $scope.Profile.ID = ID;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else {
                    ObjectFactory.putObject(Profile, 'Profile')
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

    ProfileController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('ProfileModule').controller('ProfileController', ProfileController);

}());