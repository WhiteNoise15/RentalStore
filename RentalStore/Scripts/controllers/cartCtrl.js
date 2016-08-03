(function (app) {
    "use strict";

    app.controller("cartCtrl", cartCtrl);
    cartCtrl.$inject = ["$scope", "apiService"];

    function cartCtrl($scope, apiService) {
        $scope.userId = localStorage.username.valueOf();
        console.log($scope.userId);
        apiService.get("api/cart/all/" + $scope.userId, null, succesLoad, failLoad);

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
            console.log(response);
        }


        $scope.countPrice = function () {
            apiService.get("api/cart/" + $scope.userId + "/price", null, successPrice, failPrice);
        }


        function successPrice(response) {
            console.log(response);
            alert("Спасибо за заказ. С вас " + response.data + " руб.");
        }

        function failPrice(response) {
            console.log(response);
        }
    }

})(angular.module("rentalStore"));