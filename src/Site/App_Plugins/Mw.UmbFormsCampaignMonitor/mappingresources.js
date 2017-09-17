angular.module('umbraco.resources').factory('umbFormCampMonPickerResource', function ($http,umbRequestHelper) {

    var root = '/Umbraco/backoffice/Api/ListMapping/';
    return {
        getLists: function() {
            return umbRequestHelper.resourcePromise($http.get(root + 'GetLists'), 'No lists found.');
        },
        getList: function (id) {
            return umbRequestHelper.resourcePromise($http.get(root + 'GetList/' + id), 'List ' + id + ' not found');
        }
    };
});