(function () {
    var app = angular.module('calc-app', []);

    app.factory('dataFactory', ['$http', function ($http) {

        var urlBase = '/api/Calculator';
        var dataFactory = {};

        dataFactory.getOperators = function (methodName) {
            return $http.get(urlBase + '/' + methodName);
        };

        dataFactory.postCalculate = function (obj, methodName) {
            return $http.post(urlBase + '/' + methodName, obj);
        };

        return dataFactory;
    }]);

    app.controller('calcController', ['$scope', 'dataFactory',
       function ($scope, dataFactory) {
           // scope variables
           $scope.ArgumentA = "";
           $scope.ArgumentB = "";

           $scope.Operator = "";
           $scope.operators = [];

           $scope.result = "";
           
           $scope.status = "";

           $scope.myStyle = {};
           
           $scope.exceptionMessageText = ""; 
           getInit();

           function getInit() {
               dataFactory.getOperators('GetOperations')
                   .then(function (response) {
                       $scope.operators = response.data;
                       $scope.Operator = $scope.operators[0].Id;
                   }, function (error) {
                       $scope.exceptionMessageText = 'Unable to load Operation List : ' + error.message;
                   });
           }

           $scope.btnPostCall = function () {
               if ($scope.ArgumentB === 0 && $scope.Operator === "/") {
                   $scope.exceptionMessageText = "Can't divide to zero";
                   return;
               }
               var arguments = [$scope.ArgumentA, $scope.ArgumentB];
               var operationId = $scope.Operator;
               var obj = {
                   'OperationId': operationId,
                   'Args': arguments
               };
               dataFactory.postCalculate(obj, 'Calculate')
               .then(function (response) {
                   $scope.status = 'ok';
                   $scope.result = response.data.Result;
                   var color = response.data.ColorCode;
                   if (color === "green") {
                       $scope.myStyle = { "color": "green" };}
                   else if (color === "red") {
                       $scope.myStyle = { "color": "red" };
                   }
                       $scope.exceptionMessageText = "";
                   }, function (error) {
                   $scope.exceptionMessageText = 'Unable to calculate: ' + error.message;
               });
           };

       }]);
}());