angular.module('umbraco.resources').factory('umbFormCampMonPickerResource', function ($http,umbRequestHelper) {

    var root = '/Umbraco/backoffice/Api/ListMapping/';
    return {
        getLists: function() {
            return umbRequestHelper.resourcePromise($http.get(root + 'GetLists'), 'No lists found.');
        },
        getListFields: function (id) {
            return umbRequestHelper.resourcePromise($http.get(root + 'GetListFields/?listId=' + id), 'List ' + id + ' not found');
        }
    };
});