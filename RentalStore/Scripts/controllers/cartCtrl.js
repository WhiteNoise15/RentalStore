(function (app) {
    "use strict";

    app.controller("cartCtrl", cartCtrl);
    cartCtrl.$inject = ["$scope", "apiService", "$window", "$location"];

    function cartCtrl($scope, apiService, $window, $location) {
        $scope.userId = localStorage.username.valueOf();
        apiService.get(`api/cart/all/${$scope.userId}`, null, succesLoad, failLoad);

        $scope.close = function () {
            $location.path("/movies");
        }

        function succesLoad({ data }) {
            $scope.moviesToShow = data;
        }

        function failLoad(response) {
            console.log(response);
        }

        $scope.remove = function (movie) {
            apiService.del("api/cart/" + $scope.userId + "/remove/" + movie.Id, removed, removeFail);
            let index = $scope.moviesToShow.indexOf(movie);
            $scope.moviesToShow.splice(index, 1);

        }

        function removed(response) {
            console.log(response);
        }

        function removeFail({ data }) {
            console.log(data);
        }


        $scope.countPrice = function () {
            apiService.get(`api/cart/${$scope.userId}/price`, null, successPrice, failPrice);
        }


        function successPrice({ data }) {
            $scope.moviesToShow = [];
            $scope.price = data;
            $scope.bought = true;
        }

        function failPrice({ data }) {
            console.log(data);
        }


        function removeMessage() {
            $timeout(function () {
                $scope.bought = null;
            }, 4000);
        }
    }

})(angular.module("rentalStore"));