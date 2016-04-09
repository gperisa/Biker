(function () {

    var WallController = function ($scope, ObjectFactory, Notification) {

        $scope.Wall = {};

        function init() {

            //$scope.Wall = ObjectFactory.getObject(1, 'Starter')
            //.success(function (WallData) {
            //    $scope.Wall = WallData;
            //})
            //.error(function (data, status, headers, config) {
            //    Notification.error({ message: 'Error: ' + data, delay: 2000 });
            //});
        }

        init();

        $scope.WallFormValidate = function (isValid, Wall) {
            // check to make sure the form is completely valid
            if (isValid) {
                if (Wall.ID === 0 || Wall.ID === undefined) {
                    ObjectFactory.postObject(Wall, 'Wall')
                    .success(function (ID) {

                        $scope.Wall.ID = ID;

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
                else {
                    ObjectFactory.putObject(Wall, 'Wall')
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

    WallController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('WallModule').controller('WallController', WallController);

}());