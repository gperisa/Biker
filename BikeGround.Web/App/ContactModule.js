(function () {

    var Contact = function ContactController($scope) {
        $scope.podaci = [{ Ime: 'Petar', Prezime: 'Miletić' }, { Ime: 'Goran', Prezime: 'Periša' }];
    };

    Contact.$inject = ['$scope'];

    angular.module('ContactModule').controller('ContactController', Contact);

}());