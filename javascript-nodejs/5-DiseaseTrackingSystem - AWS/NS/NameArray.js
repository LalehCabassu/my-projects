/**
 * Created by Life on 2/4/15.
 */

var nameRecord = require('./NameRecord.js');

var array = [];

module.exports = {

    AddRecord: function(name, type, host, port)
    {
        array[name] = new nameRecord(type, host, port);
    },

    GetRecord: function(name)
    {
        return array[name];
    },

    ClearArray: function() {
        array = [];
    }

    //Record: function (endpoint, name) {
    //    Array.push({
    //        key: name,
    //        value: endpoint
    //    });
    //},
    //
    //Lookup: function(name) {
    //    Array.filter(new function(element, name)
    //    {
    //        if(element.value == name)
    //            return element;
    //    });
    //}
}