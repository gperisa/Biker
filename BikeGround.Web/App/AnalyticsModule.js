
(function () {

    var AnalyticsController = function ($scope) {

        $scope.aktivan = "";

        $scope.Start = function (t) {
            $scope.aktivan = t;
        };
    };

    AnalyticsController.$inject = ['$scope'];

    angular.module('AnalyticsModule').controller('AnalyticsController', AnalyticsController);

}());