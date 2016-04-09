(function () {

    var TripController = function ($scope, ObjectFactory, Notification) {

        $scope.Trip = {};
        $scope.Trips = {};

        function init() {
            $scope.Trips = ObjectFactory.getMultiple('Trip')
            .success(function (TripData) {
                $scope.Trips = TripData;
            })
            .error(function (data, status, headers, config) {
                Notification.error({ message: 'Error: ' + status, delay: 2000 });
            });
        }

        // Dodaje trip u listu tripova 
        // kako bi se mogla koristiti dalje u aplikaciji
        function resolveTrip(ID, Trip) {
            if (!ID) {
                $scope.Trips.push(Trip);
                $scope.Global.Trips.push({ 'ID': Trip.ID, 'Title': Trip.Title });
            }
            else if (ID > 0) {
                for (var i in $scope.Trips) {
                    if ($scope.Trips[i].ID === ID) {
                        $scope.Trips[i] = Trip;
                        break;
                    }
                }

                for (var i in $scope.Global.Trips) {
                    if ($scope.Global.Trips[i].ID === ID) {
                        $scope.Global.Trips[i].Title = Trip.Title;
                    }
                }
            }
        }

        init();

        $scope.refresh = function () {
            init();
        }

        $scope.setTrip = function (ID) {

            if (ID === 0) {
                $scope.Trip = {};
                return;
            }

            for (var i in $scope.Trips) {
                if ($scope.Trips[i].ID === ID) {
                    $scope.Trip = $scope.Trips[i];
                    break;
                }
            }
        }

        $scope.TripFormValidate = function (isValid, Trip) {
            
            if (isValid) {

                Trip.BlogID = $scope.Global.BlogID;

                if (Trip.ID === 0 || Trip.ID === undefined) {
                    ObjectFactory.postObject(Trip, 'Trip')
                    .success(function (ID) {

                        $scope.Trip.ID = ID;
                        Trip.ID = ID;

                        resolveTrip(null, Trip);

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({
                        message: 'Error: ' +status, delay: 2000 });
                    });
                }
                else if (Trip.ID > 0) {
                    ObjectFactory.putObject(Trip, 'Trip')
                    .success(function () {

                        resolveTrip(Trip.ID, Trip);

                        Notification.success({ message: 'Ok!', delay: 2000 });
                    })
                    .error(function (data, status, headers, config) {
                        Notification.error({ message: 'Error: ' + status, delay: 2000 });
                    });
                }
            }
        };
    };

    TripController.$inject = ['$scope', 'ObjectFactory', 'Notification'];

    angular.module('TripModule').controller('TripController', TripController);

}());