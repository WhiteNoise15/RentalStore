(function (app) {
    "use strict";

    app.controller("cartCtrl", cartCtrl);
    cartCtrl.$inject = ["$scope", "apiService", "$window", "$location"];

    function cartCtrl($scope, apiService, $window, $location) {
        $scope.userId = localStorage.username.valueOf();
        console.log($scope.userId);
        apiService.get("api/cart/all/" + $scope.userId, null, succesLoad, failLoad);

        $scope.close = function () {
            $location.path("/movies");
        }

        function succesLoad(response) {
            $scope.moviesToShow = response.data;
            console.log(response);
        }

        function failLoad(response) {
            console.log(response);
        }

        $scope.remove = function (movie) {
            apiService.post("api/cart/" + $scope.userId + "/remove", movie, removed, removeFail);
            var index = $scope.moviesToShow.indexOf(movie);
            $scope.moviesToShow.splice(index, 1);
            
        }

        function removed(response) {
            console.log(response);
        }

        function removeFail(response) {
            alert(response.data);
        }


        $scope.countPrice = function () {
            apiService.get("api/cart/" + $scope.userId + "/price", null, successPrice, failPrice);
        }


        function successPrice(response) {
            $scope.moviesToShow = [];
            $scope.price = response.data;
            $scope.bought = true;
        }

        function failPrice(response) {
            console.log(response.data);
        }


        function removeMessage() {
            $timeout(function () {
                $scope.bought = null;
            }, 4000);
        }
    }

})(angular.module("rentalStore"));