(function (app) {
    "use strict";
    app.controller("editMovieCtrl", editMovieCtrl);

    editMovieCtrl.$inject = ["$scope", "apiService", "$stateParams", "$location", "$timeout", "$window"];

    function editMovieCtrl($scope, apiService, $stateParams, $location, $timeout, $window) {
        apiService.get("api/genres", null, loadGenres, loadGenresFailed)
        apiService.get("api/movies/details/" + $stateParams.movieId, null, movieLoaded, movieLoadedFail);

        let initialImage;
        const urlRegex = /^https?:\/\/(?:[a-z\-]+\.)+[a-z]{2,6}(?:\/[^\/#?]+)+\.(?:jpe?g|gif|png)$/;
        $scope.Image = "";

        function checkImage() {
            if (!$scope.movie)
                return;
            if ($scope.Image.match(urlRegex))
                $scope.movie.Image = $scope.Image;
            else
                $scope.movie.Image = initialImage;
        }

        $scope.$watch("Image", checkImage);

        function movieLoaded({ data }) {
            $scope.movie = data;
            initialImage = $scope.movie.Image;
        }

        function movieLoadedFail() {
            console.log("Ошибка при загрузкке фильма");
            $location.url("movies");
        }

        function loadGenres(result) {
            $scope.genres = result.data;
        }

        function loadGenresFailed(result) {
            console.log("ошибка при загрузке жанров");
            $location.url("movies");
        }

        $scope.dateOptions = { formatYear: 'yy', startingDay: 1 };
        $scope.datepicker = {};
        $scope.openDatePicker = openDatePicker;

        function openDatePicker($event) {
            $event.preventDefault(); $event.stopPropagation();
            $scope.datepicker.opened = true;
        }

        $scope.editMovie = function () {
            apiService.put('api/movies/update', $scope.movie, movieEdited, movieEditFail);
        }

        function movieEdited({ data }) {
            $scope.movie = data;
            $scope.success = true;
            $window.scrollTo(0, 0);
            removeMessage();
        }

        function movieEditFail() {
            $scope.success = false;
            removeMessage();
        }

        function removeMessage() {
            $timeout(function () {
                $scope.success = null;
            }, 4000);
        }
    }

})(angular.module("rentalStore"));