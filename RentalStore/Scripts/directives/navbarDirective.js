(function (app) {
    "use strict";

    app.directive("navBar", navBar);
    navBar.$inject = ['$state'];

    function navBar($state) {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/Scripts/directives/navbar.html',
            link: function ($scope, $element, $attrs) {
                $scope.Username = { localStorage };
                $scope.Role = { localStorage };
                $scope.reset = function () {
                    $state.reload();
                }
            }
        };
    }

})(angular.module("rentalStore"));