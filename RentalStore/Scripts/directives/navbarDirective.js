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
                $scope.Username = localStorage.Username;
                $scope.Role = localStorage.Role;
                console.log($scope.Username);
                $scope.reset = function () {
                    console.log("sds");
                    $state.reload();
                }
            }
        };
    }

})(angular.module("rentalStore"));