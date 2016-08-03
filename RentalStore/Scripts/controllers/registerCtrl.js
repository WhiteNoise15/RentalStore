(function (app) {
    "use strict";
    app.controller("registerCtrl", registerCtrl);
    registerCtrl.$inject = ["$scope", "apiService"];

    function registerCtrl($scope, apiService) {
        $scope.user = {};
        apiService.get("api/roles", null, ok, end);

        function ok(response) {
            $scope.user.Role = response.data[0];
            console.log($scope.user);
        }

        function end(response) {
            console.log(response);
        }

        $scope.register = function () {
            console.log($scope.user);
            apiService.post("api/account/register", $scope.user, registred, notRegistred);
        }

        function registred(result) {
            console.log(result);
        }

        function notRegistred(result) {
            console.log(result);
        }

    }


})(angular.module("rentalStore"));