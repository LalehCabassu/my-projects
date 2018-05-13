/**
 * Created by Life on 2/4/15.
 */

var EndPoint = require('./../lib/EndPoint.js');

var array = [];

module.exports = {

    AddName: function(name, host, port)
    {
        array[name] = new EndPoint(host, port);
    },

    RemoveName: function(name){
        delete array[name];
    },

    GetName: function(name)
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

    Clear: function() {
        array = [];
    }
}