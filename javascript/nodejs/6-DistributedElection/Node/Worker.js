/**
 * Created by Life on 3/30/15.
 */

var resourceManager = require('./ResourceManager.js');
var computeEditDistance = require('./../lib/ComputeEditDistance.js');
var webservice = require('./../lib/Webservice.js');
var DataRecord = require('./DataRecord.js');

var workerTimeout;

module.exports = {
    Active: function(){
        console.log("Worker is activated. (processId: ", process.processId, ")");
        workerTimeout = setTimeout(main, 0);
    },

    Deactive: function(){
        console.log("Worker is deactivated. (processId: ", process.processId, ")");
        clearTimeout(workerTimeout);
        workerTimeout = null;
    }
}

function main()
{
    getStringPair(function(err, data){
        if(err || data == false)
            console.log("Error in getting a string pair. Error: ", err);
        else {
            //console.log("Got a string pair -> ", data);
            saveResult(data, function(err, data){
                if(err)
                    console.log("Error in saving the result.");
                else {
                    console.log("Result saved (" + data + ").");
                    workerTimeout = setTimeout(main, 0);
                }
            });
        }
    });
}

function getStringPair(callback){
    resourceManager.GetStringPair(function(err, data){
        if(err || data == false) {
            callback(err, null);
        }
        else {
            var recordData = new DataRecord(data.recordId, data.string1, data.string2, null);
            callback(null, recordData);
        }
    });
}

function saveResult(dataRecord, callback) {
    if(dataRecord != null) {
        editDistance(dataRecord.string1, dataRecord.string2, function(err, data){
            dataRecord.distance = data;
            console.log("Saving result: ", dataRecord);
            resourceManager.SaveResult(dataRecord, callback);
        });
    }
}

function editDistance(string1, string2, callback){
    var result = computeEditDistance(string1, string2);
    if(result != null)
        callback(null, result);
}
