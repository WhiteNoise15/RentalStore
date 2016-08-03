(function () {
    "use strict";

    var app = angular.module("rentalStore", ['ui.router', 'ui.bootstrap', 'ui.router.modal', "angularValidator", "ngCookies", "LocalStorageModule"]);

    app.config(configFunction);
    app.run(run);

    configFunction.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function configFunction($stateProvider, $urlRouterProvider, $locationProvider) {

        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: "Content/Partials/index.html",
                controller: 'indexCtrl'
            })
            .state('login', {
                url: '/login',
                templateUrl: 'Content/Partials/login.html',
                controller: 'loginCtrl'
            })
            .state('register', {
                url: '/register',
                templateUrl: 'Content/Partials/register.html',
                controller: 'registerCtrl'
            })
            .state('movies', {
                url: "/movies",
                templateUrl: "Content/Partials/moviesList.html",
                controller: "moviesCtrl"
            })
            .state('movies.movie', {
                url: "/{movieId:[0-9]{1,8}}",
                modal: true,
                templateUrl: "Content/Partials/movieDetails.html",
                controller: 'movieDetailsCtrl'

            })
            .state("edit", {
                modal: false,
                url: "/movies/edit/{movieId:[0-9]{1,8}}",
                templateUrl: "Content/Partials/editMovie.html",
                controller: "editMovieCtrl"

            })
            .state("addMovie", {
                url: "/movies/add",
                templateUrl: "Content/Partials/addMovie.html",
                controller: 'addMovieCtrl'
            })
            .state("cart", {
                url: "/cart/user/{id}",
                templateUrl: "Content/Partials/cart.html",
                controller: "cartCtrl"
            });
          
        $locationProvider.html5Mode(true);
        
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http', "$window"];
    function run($rootScope, $location, $cookieStore, $http, $window) {

        $rootScope.globals = $cookieStore.get('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {

            var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
            var loggedIn = $rootScope.globals.currentUser;
            if (restrictedPage && !loggedIn) {
                $window.location.href = "/login"
            }
        });
    }



})();