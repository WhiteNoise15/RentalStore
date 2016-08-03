(function (app) {
    "use strict";
    app.controller("addMovieCtrl", addMovieCtrl);

    addMovieCtrl.$inject = ["$scope", "apiService", "$window", "$timeout"];

    function addMovieCtrl($scope, apiService, $window, $timeout) {

        var initialImage = "../../Content/images/movies/unknown.jpg";
        var urlRegex = /^https?:\/\/(?:[a-z\-]+\.)+[a-z]{2,6}(?:\/[^\/#?]+)+\.(?:jpe?g|gif|png)$/;

        $scope.genres = [];
        $scope.movie = {
            GenreId: 1,
            Count: 1,
            Image: initialImage

        };
        $scope.Image = "";
       
        function checkImage() {
            if ($scope.Image.match(urlRegex))
                $scope.movie.Image = $scope.Image;
            else
                $scope.movie.Image = initialImage;
        }

        $scope.$watch("Image", checkImage);

        $scope.dateOptions = { formatYear: 'yy', startingDay: 1 };
        $scope.datepicker = {};
        $scope.openDatePicker = openDatePicker;

        $scope.addMovie = function () {
            apiService.post("api/movies/add", $scope.movie, addMovieSucces, addMovieFail);
            $scope.Image = "";
            addMovieForm.reset();
            $window.scrollTo(0, 0);
        }

        function addMovieSucces(response) {
            $scope.movie = response.data;
            $scope.success = true;
            removeMessage();
        }

        function addMovieFail(response) {
            $scope.success = false;
            removeMessage();
        }

        function removeMessage()
        {
            $timeout(function () {
                $scope.success = null;
            }, 4000);
        }

        function openDatePicker($event) {
            $event.preventDefault(); $event.stopPropagation();
            $scope.datepicker.opened = true;
        };

        apiService.get("api/genres", null, successfull, errorfull);



        function successfull(result) {
            $scope.genres = result.data;
        }

        function errorfull(result) {
            console.log("Не удалось загрузить список жанров");
        }



    }

  

})(angular.module("rentalStore"));