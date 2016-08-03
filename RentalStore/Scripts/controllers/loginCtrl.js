﻿(function (app) {
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
            console.log($scope.user);
            authService.login($scope.user, function (response) {
                console.log("Успех");
                authService.setCredentials($scope.Username, $scope.Password, $scope.Email);
                localStorage.Username = response.data.Username;
                localStorage.username = response.data.Id;
                localStorage.Role = response.data.Role.Name;
                    $window.location.href = "/";
            }, function (response) {
                console.log(response);
            });
        };
    }

})(angular.module("rentalStore"));