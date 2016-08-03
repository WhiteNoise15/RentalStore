(function (app) {
    "use strict";

    app.directive("navBar", navBar);

    function navBar() {
        return {
            restrict: 'E',
            replace: 'true',
            templateUrl: '/Scripts/directives/navbar.html',
            link: function ($scope, $element, $attrs) {
                $scope.Username = localStorage.Username;
                $scope.Role = localStorage.Role;
            }
        };
    }

})(angular.module("rentalStore"));