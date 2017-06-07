(function (app) {
    "use strict";
    app.controller('movieDetailsCtrl', movieDetailsCtrl);
    movieDetailsCtrl.$inject = ['$scope', '$uibModal', '$uibModalInstance', "$stateParams", "apiService", "$state"];

    function movieDetailsCtrl($scope, $uibModalInstance, $uibModal, $stateParams, apiService, $state) {

        apiService.get(`api/movies/details/${$stateParams.movieId}`, null, detailsLoaded, detailsLoadFail);
        
        function detailsLoaded(result) {
            $scope.movie = result.data;
        }

        function detailsLoadFail(result) {
        }

        $scope.close = function () {
            $state.go('^');
        };

        $scope.userId = localStorage.username.valueOf();
        console.log($scope.userId);
        $scope.userRole = localStorage.Role;

        $scope.addToCart = function () {
            apiService.post("api/cart/add", { Movie: $scope.movie, UserId: $scope.userId }, added, addedFail);
            $state.go("^");

        }

        function added(response) {
            $scope.movie = response.data.Movie;
            $scope.moviesLeft = response.data.Movie.Count;
            alert("Фильм добавлен в корзину");
        }


        function addedFail(response) {
            console.log(response.data);
            alert(response.data);
        }
    }


})(angular.module('rentalStore'));