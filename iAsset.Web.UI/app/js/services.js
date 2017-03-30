
var mainApp = angular.module('iAsset.services', []);
mainApp.service('weatherService', function ($http) {
    //this.square = function (a) {
    //    return (a * a);
    //}

    this.getCountries = function(){
        return $http({
                          method: 'JSONP',
                          url: 'http://localhost:51009/api/Location/GetCountries'
                      });
    }
});