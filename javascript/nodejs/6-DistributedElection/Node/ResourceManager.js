/**
 * Created by Life on 3/30/15.
 */

var querystring = require('querystring');

var s3handler = require('./../lib/S3handler.js');
var stringUtil = require('./../lib/StringUtil.js');
var resourceManagerState = require('./ResourceManagerState.js');
var DataRecord = require('./DataRecord.js');
var nameServerClient = require('./../NS/NameServerClient.js');
var webservice = require('./../lib/Webservice.js');

var bucketName = 'cs7930-distributed-mutualexclusion-election';
var inputDirectory = 'input/StringData-';
var stateFile = 'rm-state.txt';
var outputDirectory = 'output/';

var state = null;
var inputFiles = [];            // list of input files
var currentInputData = [];      // current input file
var currentOutputData = [];     // current output file
var saveStateIntervalDelay = 3000;
var saveStateInterval = null;

s3handler.Setup('us-east-1');

module.exports = {
    Active: function (callback) {
        if (state != null && state.resourceManagerId == process.processId) {
            console.log("Resource Manager is activated. (processId: ", process.processId);

            //suppose the bucket is already set up with input data files
            s3handler.FilterObjects(bucketName, inputDirectory, function (err, files) {
                if (err)
                    console.log('Error in listing input files from s3.');
                else {
                    inputFiles = files;
                    console.log("Listed input data files on S3.");
                    remoteGetState(callback);
                    saveStateInterval = setInterval(saveState, saveStateIntervalDelay);
                }
            });
        }
    },

    GetState: function(callback){
        if(state != null && state.resourceManagerId == process.processId)
            localGetState(callback);
        else
            remoteGetState(callback);
    },

    UpdateResourceManagerId: function(processId){
        state.resourceManagerId = processId;
    },

    GetStringPair: function(callback)
    {
        if(state != null && state.resourceManagerId == process.processId)
            localGetStringPair(callback);
        else
            remoteGetStringPair(callback);
    },

    SaveResult: function(dataRecord, callback)
    {
        if(state != null && state.resourceManagerId == process.processId)
            localSaveResult(dataRecord, callback);
        else
            remoteSaveResult(dataRecord, callback);
    },

    ElectionStarted: function(callback)
    {
        if(state != null)
        {
            state.resourceManagerId = null;
            state.electionInProcess = true;
            callback(null, state);
        }
    },

    ElectionEnded: function(callback)
    {
        if(state != null){
            state.resourceManagerId = process.processId;
            state.electionInProcess = false;
            callback(null, state);
        }
    },

    Deactive: function()
    {
        console.log("Resource Manager is deactivated. (processId: ", process.processId, ")");
        clearInterval(saveStateInterval);
        saveStateInterval = null;
    }
};

function localGetStringPair(callback){
    if(currentInputData != [] && state.readRecord != undefined) {
        retrieveInputFile(function (err, data) {
            if (err) {
                callback(err, null);
            }
            else {
                var dataRecord = DataRecord.fromString(currentInputData[state.readRecord]);
                state.readRecord++;
                callback(null, dataRecord);
            }
        });
    }
    else
        callback("Not activated completely.", null);
}

function remoteGetStringPair(callback){
    var nshost = process.argv[3];
    var nsport = parseInt(process.argv[4]);
    nameServerClient.Setup(nshost, nsport);

    // Query for the resource manager
    nameServerClient.Query(state.resourceManagerId, function (error, data) {
        if (error != null)
            console.log("Error in querying for the resource manager.")
        else if(data == false)
            console.log("Resource manager is not found.");
        else {
            var json = JSON.stringify("");
            var post_data = querystring.stringify({getStringPair: json});
            webservice(data.host, data.port, '/getstringpair', post_data, callback, 50, 10);
        }
    });
}

function localSaveResult(dataRecord, callback)
{
    // duplicate ?????
    // missing ????

    var outputFile;
    // start a new output file
    if(state.writeRecord < 0 || state.writeRecord >= 100)
    {
        ++state.outputFileIndex;
        currentOutputData = [];
        state.writeRecord = 0;
    }
    outputFile = outputDirectory + 'StringData-' + stringUtil.NumberPadder(state.outputFileIndex, 3) + '.csv';

    s3handler.GetFileString(bucketName, outputFile, function (err, data) {
        if (err)
            currentOutputData = [];
        else
            currentOutputData = data.toString().split('\n');

        currentOutputData[state.writeRecord] = dataRecord;
        state.writeRecord++;

        var outputData = '';
        for(var index in currentOutputData)
            outputData += currentOutputData[index].toString() + '\n';

        console.log("Uploading output file (" +  outputFile + ")");
        saveFile(outputFile, outputData, callback);
    });
}

function remoteSaveResult(dataRecord, callback){
    var nshost = process.argv[3];
    var nsport = parseInt(process.argv[4]);
    nameServerClient.Setup(nshost, nsport);

    var json = dataRecord.toJSON();
    var post_data = querystring.stringify({saveResult: json});

    // Query for the resource manager
    nameServerClient.Query(state.resourceManagerId, function (error, data) {
        if (error != null)
            console.log("Error in querying for the resource manager.")
        else {
            webservice(data.host, data.port, '/saveresult', post_data, callback, 10, 10);
        }
    });
}

function localGetState(callback){
    callback(null, state);
}

function remoteGetState(callback){
    s3handler.GetFileString(bucketName, stateFile, function (err, data) {
        if (err) {
            state = new resourceManagerState(null, -1, -1, -1, -1, false);
            s3handler.UploadTextFile(bucketName, stateFile, state.toJSON(), function (err, data) {
                if (err) {
                    console.log("Error in uploading file(" + stateFile + "). \nMessage: ", err);
                    callback(err, null);
                }
                else {
                    console.log("Successfully uploaded file(" + stateFile + ")");
                    callback(null, state);
                }
            });
        }
        else {
            state = resourceManagerState.fromJSON(data);
            state.resourceManagerId = process.processId;
            console.log("Successfully got file(" + stateFile + ")");
            callback(null, state);
        }
    });
}

function saveState(callback) {
    if(state != null) {
        saveFile(stateFile, state.toJSON(), function (err, data) {
            if (err) {
                console.log("Error in saving state file. State: ", state.toJSON());
                if(callback != null && callback != undefined)
                    callback(err, false);
            }
            else {
                console.log("Saved the state file. State: ", state.toJSON());
                if(callback != null && callback != undefined)
                    callback(null, true);
            }
        });
    }
}

function saveFile(fileName, content, callback)
{
    s3handler.UploadTextFile(bucketName, fileName, content, function(err, data){
        if (err) {
            callback(err, fileName);
        }
        else {
            callback(null, fileName);
        }
    });
}

function retrieveInputFile(callback) {
    if(state.inputFileIndex < 0)
        state.inputFileIndex = 0;

    if ((state.readRecord < 0 || state.readRecord >= 100) &&
         (state.inputFileIndex < inputFiles.length)) {
        ++state.inputFileIndex;
        state.readRecord = 0;
    }

    if(currentInputData.length == 0){
        var fileName = inputFiles[state.inputFileIndex];
        if(fileName != undefined) {
            s3handler.GetFileString(bucketName, fileName, function (err, data) {
                if (err) {
                    console.log("Error in getting file(" + fileName + "). \nMessage: ", err);
                    callback(err, null);
                }
                else {
                    console.log("Successfully got file(" + fileName + ")");
                    currentInputData = data.toString().split('\n');
                    callback(null, currentInputData);
                }
            });
        }
    }
    else
        callback(null, currentInputData);
}
