
(function () {

    var SettingsController = function ($scope) {

        $scope.aktivan = "";

        $scope.Start = function (t) {
            $scope.aktivan = t;
        };
    };

    SettingsController.$inject = ['$scope'];

    angular.module('SettingsModule').controller('SettingsController', SettingsController);

}());