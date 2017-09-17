angular.module("umbraco").controller("Mw.UmbFormsCampaignMonitor.Mapping.Controller", function ($scope, $http, $routeParams, pickerResource, umbFormCampMonPickerResource) {
    "use strict";
    var defaultMappings = [
        {
            listField: "Email Address",
            field: "",
            staticValue: ""
        },
        {
            listField: "Subscribe",
            field: "",
            staticValue: ""
        },
        {
            listField: "Name",
            field: "",
            staticValue: ""
        },
        {
            listField: "Lastname",
            field: "",
            staticValue: ""
        }];

    function save() {
        $scope.setting.value = JSON.stringify({
            mappings: $scope.mappings,
            listId: $scope.listId
        });
    }

    function init() {
        if (!$scope.setting.value) {
            $scope.mappings = defaultMappings;
            $scope.lists = [];
            $scope.setting.value = {
                mappings: $scope.mappings,
                listId: null
            }
        } else {
            var value = JSON.parse($scope.setting.value);
            $scope.listId = value.listId;
            $scope.mappings = value.mappings;
            if ($scope.listId != null) {
                $scope.changeList();
            }
        }
        umbFormCampMonPickerResource.getLists().then(function (response) {
            $scope.lists = response;
            if ($scope.listId) {
                $scope.changeList();
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

    $scope.isReservedFieldName = function (fieldName) {
        return fieldName === "Email Address" || fieldName === "Name" || fieldName === "Lastname" || fieldName === "Subscribe";
    };

    $scope.changeList = function () {
        if ($scope.listId == null) {
            $scope.mappings = defaultMappings;
            save();
            return;
        }
        save();
        umbFormCampMonPickerResource.getListFields($scope.listId).then(function (response) {
            $scope.listFields = response;
        });

    };

    $scope.addMapping = function () {
        $scope.mappings.push({
            listField: "",
            field: "",
            staticValue: ""
        });
    };

    $scope.deleteMapping = function (index) {
        $scope.mappings.splice(index, 1);
        save();
    };

    $scope.stringifyValue = function () {
        save();
    };

    init();
});