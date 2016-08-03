(function (app) {
    app.directive("availableMovie", availableMovie);

    function availableMovie() {
        return {
            restict: "E",
            templateUrl: "/Scripts/directives/available.html",
            link: function ($scope, $element, $attrs) {
                $scope.getAvailableClass = function () {
                    if ($attrs.isAvailable === "true")
                        return 'label label-success'
                    else
                        return 'label label-danger'
                };

                $scope.getAvailability = function () {
                    if ($attrs.isAvailable === 'true')
                        return "Есть в наличии"
                    else
                        return "Отсутствует"
                };
            }
        };
    }

})(angular.module("rentalStore"));