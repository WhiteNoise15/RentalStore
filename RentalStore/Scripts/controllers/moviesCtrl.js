(function (app) {
    "use strict";

    app.controller("moviesCtrl", moviesCtrl);

    moviesCtrl.$inject = ['$scope', '$log', "apiService", "$timeout"];

    function moviesCtrl($scope, $log, apiService, $timeout) {
        $scope.itemsPerPage = 12;
        $scope.moviesToShow = [];
        $scope.currentPage = 1;
        
        $scope.getData = function() {
            const config = {
                params: {
                    currentPage: $scope.currentPage,
                    itemsPerPage: $scope.itemsPerPage,
                    filter: $scope.filterMovies
                }
            }
            apiService.get("api/movies/", config, moviesLoaded, moviesLoadFailed);
        }

        $scope.clear = function () {
            $scope.filterMovies = "";
            $scope.getData();
        }

        $scope.pageChanged = function() {
            $scope.getData();
        };

        function moviesLoaded(result) {
            $scope.moviesToShow = result.data.moviesToShow;
            $scope.totalItems = result.data.totalItems;
            const hasMovies = $scope.moviesToShow.length > 0 || $scope.totalItems > 0;

            if (!hasMovies)
                $scope.message = "Фильмов не найдено";
            removeMessage();
        }

        function moviesLoadFailed() {
            $scope.message = "Ошибка на сервере";
            removeMessage();
        }

        function removeMessage() {
            $timeout(function () {
                $scope.message = null;
            }, 4000);
        }
        
        $scope.getData();
    }

})(angular.module("rentalStore"));