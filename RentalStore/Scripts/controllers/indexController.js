(function (app) {
    "use strict";

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ["$scope", "apiService", "$rootScope", "localStorageService"];

    function indexCtrl($scope, apiService, $rootScope, localStorageService) {
        $scope.latestMovies = [];

        localStorageService.clearAll();

        apiService.get("api/movies/latest", null, moviesLoaded, moviesLoadFailed);

        function moviesLoaded({data}) {
            $scope.latestMovies = data;
            if ($scope.latestMovies.length == 0) {
                $scope.message = "База фильмов пуста"
            }

        }

        function moviesLoadFailed() {
            console.log("Ошибка");
        }

    }

})(angular.module('rentalStore'));