/**
 * Created by Life on 2/10/15.
 */

var AWS = require('aws-sdk');
var instances = require('./Instances.js');

AWS.config.region = 'us-east-1';
var ec2 = new AWS.EC2();

module.exports = {
    Do: function()
    {
        stopInstances();
    }
}

function stopInstances()
{
    ec2.describeInstances(function (error, data) {
        if (error) {
            console.log("Failed listing instances: " + error);
        }
        else {
            var params = { InstanceIds: instances.GetInstanceIds() };
            ec2.stopInstances(params, function(err, data) {
                if (err)
                    console.log(err, err.stack); // an error occurred
                else
                    console.log('Stopping instances...');
            });
        }
    });
}