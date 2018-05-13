/**
 * Created by Life on 3/30/15.
 */

var querystring = require('querystring');

var resourceManager = require('./ResourceManager.js');
var worker = require('./Worker.js');
var ElectionMessage = require('./ElectionMessage.js');
var nameServerClient = require('./../NS/NameServerClient.js');
var webservice = require('./../lib/Webservice.js');

var electionTimeout;

module.exports = {

    Active:function()
    {
        electionTimeout = setTimeout(isResourceManagerAlive, 0);
    },

    ProcessElectionMessage: function(processId){
        distributeElectionMessage(processId, function(err, data){
            if(err)
                console.log("ProcessId: " + process.processId + "-> Error in forwarding election message. (processId: "+ processId + ")");
            //else
            //    console.log("ProcessId: " + process.processId + "-> Forwarded election message. (processId: "+ processId + ")");
        });
    },

    ProcessElectedMessage: function(processId){
        distributeElectedMessage(processId, function(err, data){
            if(err)
                console.log("ProcessId: " + process.processId + "-> Error in forwarding elected message. (processId: "+ processId + ")");
            //else
            //    console.log("ProcessId: " + process.processId + "-> Forwarded elected message. (processId: "+ processId + ")");
        });
    },

    Deactive: function() {
        clearTimeout(electionTimeout);
        electionTimeout = null;
    }
}


function isResourceManagerAlive(){
    var state = null;
    // no need to sync with s3
    resourceManager.GetState(function(err, data){
        if(err)
            startElection();
        else
        {
            state = data;
            if(state != null)
                if(!state.electionInProcess) {
                    //if (state.resourceManagerId != null)
                    //    if(state.resourceManagerId > process.processId)
                    //        nameServerClient.Query(state.resourceManagerId, function (err, data) {
                    //            if (err)
                    //                startElection();
                    //            else if (data == false)
                    //                startElection();
                    //            else
                    //                worker.Active();
                    //        });
                    //    else
                    //        resourceManager.Active(function (err, data) {  });
                    //else
                    //    startElection();
                    //if(state.resourceManagerId != process.processId)
                        startElection();
                }
            else
                startElection();
        }
    });
}

function startElection()
{
    resourceManager.ElectionStarted(function(err, data){
        if(err)
            console.log("Error in starting the election. (processId: " + process.processId + ")");
        else {
            resourceManager.Deactive();
            worker.Deactive();
            console.log("Sending the Election message. (processId: " + process.processId + ")");
            sendMessage(process.processId, '/election');
        }
    });
}

function distributeElectionMessage(processId, callback)
{
    if (process.processId > processId) {
        console.log("Sending the Election message. (processId: " + processId + ")");
        sendMessage(process.processId, '/election', callback);
    }
    else if(process.processId == processId) {
        console.log("Starting the Elected message. (processId: " + processId + ")");
        sendMessage(processId, '/elected', callback);
    }
    else {
        console.log("Forwarding the Election message. (processId: " + processId + ")");
        sendMessage(processId, '/election', callback);
    }
}

function distributeElectedMessage(processId, callback)
{
    if (process.processId > processId) {
        console.log("Starting a now election. (processId: " + processId + ")");
        startElection(process.processId, callback);
    }
    else if(process.processId == processId) {
        resourceManager.ElectionEnded(function (err, data) {
            if(err) {
                console.log("Error in ending the election. (processId: " + processId + ")");
                electionTimeout = setTimeout(isResourceManagerAlive, 0);
            }
            else {
                console.log("Resource Manager started. (processId: " + processId + ")");
                resourceManager.UpdateResourceManagerId(processId);
                resourceManager.Active(callback);
                //electionTimeout = setTimeout(isResourceManagerAlive, 3000);
            }
        });
    }
    else {
        console.log("Forwarding the Elected message. (processId: " + processId + ")");
        sendMessage(processId, '/elected', callback);
        resourceManager.Deactive();
        resourceManager.UpdateResourceManagerId(processId);
        worker.Active();
        electionTimeout = setTimeout(isResourceManagerAlive, 30000);
    }
}

function sendMessage(processId, route, callback){
    var message = new ElectionMessage(processId);
    var json = message.toJSON();
    var post_data = querystring.stringify({ electionMessage: json} );
    nameServerClient.Neighbor(process.processId, function(err, data){
        if(err)
            console.log('Error in getting neighbor (processId:' + process.processId + ')');
        else
            webservice(data.host, data.port, route, post_data, callback, 50, 10);
    });
}
