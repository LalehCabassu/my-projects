/**
 * Created by Life on 2/4/15.
 */

var nameRecord = require('./NameRecord.js');

var array = [];
var processId = 0;

module.exports = {

    AddNode: function(host, port)
    {
        array[processId] = new nameRecord(host, port);
        return processId++;
    },

    RemoveNode: function(processId){
        delete array[processId];
    },

    GetNode: function(processId)
    {
        var result = array[processId];

        if(result == null || result == undefined)
        {
            return null;
        }
        return result;
    },

    GetNeighbor: function (processId) {
        var result = null;

        var neighborId;
        if(processId == array.length - 1)
            neighborId = 0;
        else
            neighborId = processId + 1;

        while(result == null || result == undefined || !result.isAlive()) {
            result = array[(neighborId++) % array.length];
        }
      return result;
    },

    GetLength: function()
    {
        return array.length;
    },

    ClearArray: function() {
        array = [];
    }
}