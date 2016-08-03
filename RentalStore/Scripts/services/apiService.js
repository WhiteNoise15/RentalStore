(function(app) {
	 
	 app.factory('apiService', apiService);
	 
	 apiService.$inject = ['$http', '$location', '$rootScope'];

	 function apiService($http, $location, $rootScope) {
	 	var service  = {
	 		get: get,
	 		post: post
	 	};

	 	function get(url, config, success, failure) {
	 		return $http.get(url, config).then(function(result) {
	 			success(result);
	 		}, function(error) {
	 			if (error.status === "401") {
	 				console.log("Вы не авторизованы");
	 				$rootScope.previousState = $location.path();
	 				$location.path('/login');
	 			}
	 			else if (failure !== null) {
	 				failure(error);
	 			}
	 		});
	 	}

	 	function post (url, data, success, failure) {
	 	    return $http.post(url, data).then(function (result) {
	 	        success(result);
	 	    }, function (error) {
	 	        if (error.status === "401") {
	 	            console.log("Вы не авторизованы");
	 	            $rootScope.previousState = $location.path();
	 	            $location.path('/login');
	 	        }
	 	        else if (failure != null) {
	 	            failure(error);
	 	        }
	 	    });
	 		
	 	}

	 	return service;
	 }

})(angular.module('rentalStore'));