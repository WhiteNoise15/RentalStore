(function (app) {
    "use strict";
    app.controller("registerCtrl", registerCtrl);
    registerCtrl.$inject = ["$scope", "apiService", "$timeout", '$location'];

    function registerCtrl($scope, apiService, $timeout, $location) {
        $scope.user = {};
        $scope.user.Role = null;

        $scope.register = function () {
            apiService.post("api/account/register", $scope.user, registred, notRegistred);
        }

        function registred(result) {
            registerForm.reset();
            $scope.success = true;
        }

        function notRegistred(result) {
            $scope.error = true;
            $scope.message = result.data;
        }

        function removeMessage() {
            $timeout(function () {
                $scope.success = null;
                $scope.message = null;
                $location.path('/login');
            }, 3000);
        }
    }

})(angular.module("rentalStore"));