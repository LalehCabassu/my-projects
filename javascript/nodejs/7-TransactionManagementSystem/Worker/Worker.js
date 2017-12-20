/**
 * Created by Life on 3/30/15.
 */

var fs = require('fs');
var nsClient = require('./../NS/NameServerClient.js');
var tmClient = require('./../TransactionManager/TransactionManagerClient.js');
var rmClient = require('./../ResourceManager/ResourceManagerClient.js');

var tranId = null;
var resources = [];
var resourceManagers = [];
var statements = [];

module.exports = {
    Active: function(nshost, nsport, tmhost, tmport){
        nsClient.Setup(nshost, nsport);
        tmClient.Setup(tmhost, tmport);
        tranId = null;
        resources = [];
        resourceManagers = [];
        statements = [];
    },

    Run: function(path, callback)
    {
        fs.readFile(path, {encoding: 'utf8'}, function(err, data) {
            if (err)
                callback(err, false);
            else {
                statements = data.toString().split('\n');
                if(statements.length > 0)
                    parseStatement(0, callback);
                else
                    console.log("File is empty.");
            }
        });
    }
}

function parseStatement(index, callback) {
    if (index < statements.length) {
        var l = statements[index];
        var params = l.split(' ');
        if (params.length > 0) {
            var op = params[0].toUpperCase();
            switch (op) {
                case 'START':
                    start(function (err, data) {
                        if (err) {
                            console.log("Error in starting a transaction.");
                            abort(callback);
                        }
                        else {
                            tranId = data;
                            console.log("Transaction started (tranId:" + tranId + ").");
                            setTimeout(parseStatement, 0, ++index, callback);
                        }
                    });
                    break;
                case 'LOCK-READ':
                    if (params.length >= 3)
                        lock('read', params[1], params[2], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                case 'LOCK-WRITE':
                    if (params.length >= 3)
                        lock('write', params[1], params[2], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                case 'READ':
                    if (params.length >= 3)
                        read(params[1], function (err, data) {
                            if (data == null)
                                abort(callback);
                            else {
                                addResource(params[2], data);
                                setTimeout(parseStatement, 0, ++index, callback);
                            }
                        });
                    break;
                case 'WRITE':
                    if (params.length >= 3) {
                        var newValue = getResource(params[2]);
                        write(params[1], newValue, function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    }
                    break;
                case 'SUBSTR':
                    if (params.length >= 4)
                        substring(params[1], params[2], params[3], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                    break;
                case 'CONCAT':
                    if (params.length >= 4) {
                        var value1 = getResource(params[2]);
                        var value2 = getResource(params[3]);
                        concatenate(params[1], value1, value2, function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    }
                    break;
                case 'TRUNC':
                    if (params.length >= 3)
                        truncate(params[1], params[2], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                case 'UPPER':
                    if (params.length >= 2)
                        toUpper(params[1], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                case 'LOWER':
                    if (params.length >= 2)
                        toLower(params[1], function (err, data) {
                            if (data != true)
                                abort(callback);
                            else
                                setTimeout(parseStatement, 0, ++index, callback);
                        });
                    break;
                case 'SLEEP':
                    if (params.length >= 2)
                        setTimeout(parseStatement, params[1], ++index, callback);
                    break;
                case 'COMMIT':
                    if (params.length >= 1)
                        commit(function (err, data) {
                            if (data != true)
                                abort(callback);
                        });
                    break;
                case 'ABORT':
                    if (params.length >= 1)
                        abort(function (err, data) {
                            callback(err, data);
                        });
                    break;
                default:
                    setTimeout(parseStatement, 0, ++index, callback);
                    break;
            }
        }
        else
            setTimeout(parseStatement, 0, ++index, callback);
    }
    else
        console.log("Parsing the file is over.");
}

function start(callback)
{
    tmClient.Start(function(err, data){
        callback(err, data);
    });
}

function lock(type, resourceName, timeout, callback)
{
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else
        {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);
            rmClient.Lock(tranId, type, timeout, function (error, data) {
                if(err)
                    console.log("Error in getting read lock (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function read(resourceName, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.Read(tranId, function (error, data) {
                if (err)
                    console.log("Error in reading the resource (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function write(resourceName, newValue, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.Write(tranId, newValue, function (error, data) {
                if (err)
                    console.log("Error in writing the resource (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function concatenate(resourceName, value1, value2, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.Concatenate(tranId, value1, value2, function (error, data) {
                if (err)
                    console.log("Error in concatenating (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function substring(resourceName, index1, index2, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.SubString(tranId, index1, index2, function (error, data) {
                if (err)
                    console.log("Error in substringing (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function truncate(resourceName, index, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.Truncate(tranId, index, function (error, data) {
                if (err)
                    console.log("Error in truncating (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function toUpper(resourceName, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.ToUpper(tranId, function (error, data) {
                if (err)
                    console.log("Error in touppering the resource (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function toLower(resourceName, callback){
    nsClient.QueryResourceManager(resourceName, function(err, data){
        if(err)
            console.log("Error in querying resource manager of " + resourceName);
        else {
            var rmName = data.resourceManager;
            var rmhost = data.host;
            var rmport = data.port;

            addResourceManager(rmName);
            rmClient.Setup(rmhost, rmport);

            rmClient.ToLower(tranId, function (error, data) {
                if (err)
                    console.log("Error in tolowering the resource (rm: " + rmName + ")");
                else
                    callback(null, data);
            });
        }
    });
}

function getResource(name){
    return resources[name];
}

function addResource(name, value){
    resources[name] = value;
}

function addResourceManager(name){
    resourceManagers[name] = true;
}

function getResourceManagers(){
    return Object.keys(resourceManagers);
}

function commit(callback)
{
    var rmNames = getResourceManagers();
    tmClient.Commit(tranId, rmNames, function (err, data) {
        if(err) {
            console.log("Error in committing transaction (id: " + tranId + ")");
            callback(err, null);
        }
        else if(data == false) {
            console.log("Committing failed. (tranId: " + tranId + ")");
            callback(err, false);
        }
        else {
            console.log("Committed . (tranId: " + tranId + ")");
            callback(err, true);
        }
    });
}

function abort(callback)
{
    var rmNames = getResourceManagers();
    tmClient.Abort(tranId, rmNames, function (err, data) {
        if(err) {
            console.log("Error in aborting transaction (id: " + tranId + ")");
            callback(err, null);
        }
        else if(data == false) {
            console.log("Aborting failed. (tranId: " + tranId + ")");
            callback(err, false);
        }
        else {
            console.log("Aborted . (tranId: " + tranId + ")");
            callback(err, true);
        }
    });
}