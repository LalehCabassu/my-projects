/**
 * Created by Life on 4/21/15.
 */

var EndPoint = require('./../lib/EndPoint.js');

var array = [];

module.exports = {

    Add: function(name, host, port)
    {
        array[name] = new EndPoint(host, port);
    },

    Remove: function(name){
        delete array[name];
    },

    Get: function(name)
    {
        var result = array[name];

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

    GetNames: function(){
        return Object.keys(array);
    },

    Clear: function() {
        array = [];
    }
}