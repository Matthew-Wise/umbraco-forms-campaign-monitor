angular.module("umbraco").controller("Mw.UmbFormsCampaignMonitor.Mapping.Controller", function ($scope, $http, $routeParams, pickerResource, umbFormCampMonPickerResource) {
    "use strict";
    function init() {
        if (!$scope.setting.value) {
            $scope.mappings = [];
            $scope.lists = [];
        } else {            
            var value = JSON.parse($scope.setting.value);           
            $scope.listId = value.listId;
            $scope.mappings = value.mappings;
            
            $scope.lists = [];
        }
        umbFormCampMonPickerResource.getLists().then(function (reponse) {
            console.log(response);
            $scope.lists = response.data;
            if ($scope.listId) {                
            }
        });

        var formId = $routeParams.id;
        if (formId === -1 && $scope.model && $scope.model.fields) {
        } else {
            pickerResource.getAllFields($routeParams.id).then(function (response) {
                $scope.fields = response.data;
            });
        }
    }

    $scope.addMapping = function () {
        $scope.mappings.push({
            listField: "",
            field: ""
        });
    };

    $scope.deleteMapping = function (index) {
        $scope.mappings.splice(index, 1);
        $scope.setting.value.mappings = JSON.stringify($scope.mappings);
    };

    $scope.stringifyValue = function () {
        $scope.setting.value.mappings = JSON.stringify($scope.mappings);
    };

    init();
});