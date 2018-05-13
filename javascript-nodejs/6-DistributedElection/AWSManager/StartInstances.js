/**
 * Created by Life on 2/10/15.
 */

var AWS = require('aws-sdk');
var instances = require('./Instances.js');

AWS.config.region = 'us-east-1';
var ec2 = new AWS.EC2();
//var reservationId = "r-a6812247";
var instanceList;

module.exports = {
    Start: function()
    {
        startInstances(listInstances);
    },

    ListInstances: function()
    {
        listInstances();
    }
}

function startInstances(callback)
{
    ec2.describeInstances(function (error, data) {
        if (error) {
            console.log("Failed listing instances: " + error);
        }
        else {
            var instanceIds = [];
            //for (var i = 0; i < data.Reservations.length; i++) {
            //    if (data.Reservations[i].ReservationId === reservationId) {
                    instanceList = data.Reservations[0].Instances;
                    console.log("Instances(" + instanceList.length + "): ");
                    for (var i = 0; i < instanceList.length; i++) {
                        var instanceId = instanceList[i].InstanceId;
                        console.log('#' + i + '\t\tinstance id: ' + instanceId);
                        if (instanceList[i].State.Name != 'running')
                            instanceIds.push(instanceId);
                    }
                //}
                var params = {InstanceIds: instanceIds};
                ec2.startInstances(params, function (err, data) {
                    if (err)
                        console.log(err, err.stack); // an error occurred
                    else
                    {
                        console.log('Launching instances...');
                        callback();
                    }
                });
            //}
        }
    });
}

function listInstances()
{
    ec2.describeInstances(function (error, data) {
        if (error) {
            console.log("Failed listing instances: " + error);
        }
        else {
            //for (var i = 0; i < data.Reservations.length; i++) {
            //    if (data.Reservations[i].ReservationId === reservationId) {
            instanceList = data.Reservations[0].Instances;
            //if(systemReady()) {
                console.log("Instances(" + instanceList.length + "): ");
                for (var i = 0; i < instanceList.length; i++) {
                    var instanceId = instanceList[i].InstanceId;
                    var publicDnsName = instanceList[i].PublicDnsName;
                    console.log('#' + i + '\t\tinstance id: ' + instanceId +
                    '\t\tpublicDnsName: ' + publicDnsName);
                    instances.AddInstance(instanceId, publicDnsName);
                }
            }
        //}
        //}
        //}
    });
}

function systemReady()
{
    var running = true;
    do{
        for(var i = 0; i < instanceList.length; i++)
            if(instanceList[i].State.Name != 'running')
                running = false;
    }while(!running);

    return running;
}