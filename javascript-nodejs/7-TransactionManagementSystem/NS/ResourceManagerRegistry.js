/**
 * Created by Life on 4/16/15.
 */

var array = [];

module.exports = {

    Add: function(resource, resourceManager)
    {
        array[resource] = resourceManager;
    },

    Remove: function(resource){
        delete array[resource];
    },

    Get: function(resource)
    {
        var result = array[resource];

        if(result == null || result == undefined)
        {
            return null;
        }
        return result;
    },

    GetLength: function()
    {
        return array.length;
    },

    Clear: function() {
        array = [];
    }
}
