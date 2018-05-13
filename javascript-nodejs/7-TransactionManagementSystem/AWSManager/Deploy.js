/**
 * Created by Life on 2/10/15.
 */

var fs = require('fs');
var cp = require('child_process');
var instances = require('./Instances.js');
var ExecuteRemoteCommand = require('./ExecuteRemoteCommand.js');

var path = '/Users/Life/Documents/workspaces/nodejs/hw2';
var bucket = 's3://cs7930';
var instancePath = 'hw2';

module.exports =
{
    Do: function()
    {
        uploadProject();
    }
}

function uploadProject()
{
    console.log('Pushing to s3...');
    var exec = cp.exec;
    var command = 'aws s3 sync ' + path + ' ' + bucket +  ' --recursive --delete';
    exec(command, function(error, stdout, stderr) {
        console.log('stdout: ', stdout);
        console.log('stderr: ', stderr);
        if (error)
            console.log('exec error: ', error);
        else
            deploy(bucket, instancePath);
    });
}

function deploy(bucket, path) {
    console.log('Deploying the program to all instances...');
    for (var i = 0; i < instances.GetLength(); i++) {
        deployProject(i, bucket, path);
    }
}

function deployProject(instanceIndex, bucket, path)
{
    var command = 'aws s3 sync ' + bucket + ' ' + path + ' --delete; echo "DONE"';
    var message = 'Sync Copy HW2 from S3 (instances[' + instanceIndex + '])';
    ExecuteRemoteCommand(instanceIndex, command, message);
}