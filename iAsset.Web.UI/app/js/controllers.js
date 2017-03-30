var mainApp = angular.module('iAsset.controllers', ['ngMaterial']);

mainApp.controller('weatherController', function ($scope, $http) {
    
    $scope.getCountries = function () {
        
        $http.get('http://localhost:51009/api/Location/GetCountries').then(function (response) {
          $scope.Countries = response.data;
        });
    }
    
    $scope.getCities = function () {
        var selectedCountry = $scope.selectedCountry;
        $http.get('http://localhost:51009/api/Location/GetCities?Country='+selectedCountry).then(function (response) {
            $scope.Cities = response.data;
        });
    }

    $scope.getWeather = function () {
        var selectedCountry = $scope.selectedCountry;
        var selectedCity = $scope.selectedCity;
        console.log(selectedCity);

        $http.get('http://localhost:51009/api/Weather/GetCityWeather?City=' + selectedCity + '&Country=' + selectedCountry).then(function (response) {
            $scope.hourlyWeather = response.data;
        });
    }
});