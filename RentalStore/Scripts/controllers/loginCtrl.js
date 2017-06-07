(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ["$scope", '$location', 'authService', "localStorageService", "$window", "$state"];
    function loginCtrl($scope, $location, authService, localStorageService, $window, $state) {
        $scope.user = {};
        $scope.login = login;


        (function initController() {
            // reset login status
            authService.clearCredentials();
        })();

        function login() {
            authService.login($scope.user, function ({data}) {
                authService.setCredentials($scope.Username, $scope.Password, $scope.Email);
                localStorage.Username = data.Username;
                localStorage.username = data.Id;
                localStorage.Role = data.Role.Name;
                    $window.location.href = "/";
            }, function () {
            });
        };
    }

})(angular.module("rentalStore"));