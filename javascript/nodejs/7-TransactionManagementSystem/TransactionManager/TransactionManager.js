/**
 * Created by Life on 4/19/15.
 */

var webservice = require('./../lib/Webservice.js');
var Transaction = require('./Transaction.js');
var ResourceManagerRegistry = require('./ResourceManagerRegistry.js');
var ResourceManagerClient = require('./../ResourceManager/ResourceManagerClient.js');

var nextTransactionId = 0;
var transactions = [];

module.exports = {

    Active: function()
    {
        nextTransactionId = 0;
        transactions = [];
        setTimeout(abortDeadTransactions, 60000, 60000);
    },

    Start: function () {
        var newTransactoin = new Transaction(nextTransactionId);
        transactions[newTransactoin.id] = newTransactoin;

        ++nextTransactionId;

        return newTransactoin.id;
    },

    Commit: function (tranId, resourceManagerNames, callback) {

        transactions[tranId].resourceManagers = resourceManagerNames;

        resourceManagerService(tranId, 'commit', function(err, data){
            if(err || data == false)
                callback(null, false);
            else if(data == true) {
                transactions[tranId].alive = false;
                callback(null, true);
            }
        });
    },

    Abort: function(tranId, resourceManagerNames, callback)
    {
        transactions[tranId].resourceManagers = resourceManagerNames;

        resourceManagerService(tranId, 'abort', function(err, data){
            if(err || data == false)
                callback(null, false);
            else if(data == true) {
                transactions[tranId].alive = false;
                callback(null, true);
            }
        });
    }
}

function resourceManagerService(tranId, action, callback){
    var result = true;
    var transaction = transactions[tranId];
    var length = transaction.resourceManagers.length;
    var counter = length;
    for(var i = 0; i < length; i++)
    {
        var name = transaction.resourceManagers[i];
        if(name != null && name != undefined) {
            var rm = ResourceManagerRegistry.Get(name);
            console.log("Send a " + action + " message. (RM: " + name + "["+ rm.toString() +"])");
            ResourceManagerClient.Setup(rm.host, rm.port);
            if (action == 'commit')
                ResourceManagerClient.Unlock(tranId, function (err, data) {
                    if (err) {
                        result = false;
                        callback(err, result);
                    }
                    else if (data == true)
                        --counter;
                    if (counter == 0 && result) {
                        callback(null, result);
                    }
                });
            else if (action == 'abort')
                ResourceManagerClient.Rollback(tranId, function (err, data) {
                    if (err) {
                        result = false;
                        callback(err, result);
                    }
                    else if (data == true)
                        --counter;
                    if (counter == 0 && result) {
                        callback(null, result);
                    }
                });
        }
    }
}

function abortDeadTransactions(timeout){

    for(var i = 0; i < transactions.length; i++){
        var tran = transactions[i];
        if(tran.alive == true && tran.killingCounter == 0) {
            var runningTime = Date.now() - tran.startTimeStamp;
            if (runningTime > timeout) {
                var names = ResourceManagerRegistry.GetNames();
                tran.killingCounter = names.length;
                for (var i = 0; i < names.length; i++) {
                    var name = names[i];
                    if (name != null && name != undefined) {
                        var rm = ResourceManagerRegistry.Get(name);
                        console.log("Send an abort message. (RM: " + name + "["+ rm.toString() +"])");
                        ResourceManagerClient.Setup(rm.host, rm.port);
                        ResourceManagerClient.Rollback(tran.id, function (err, data) {
                            --tran.killingCounter;
                            if (tran.killingCounter == 0) {
                                tran.alive = false;
                                console.log("Transaction killed. (tranId: " + tran.id + ")");
                            }
                        });
                    }
                }
            }
        }
    }
    setTimeout(abortDeadTransactions, timeout, timeout);
}