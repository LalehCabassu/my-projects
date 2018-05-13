/*
    ExecuteRemoteCommand

    This function executes a command on a remote system via ssh.

    Parameters:
        instance    An instance object as found in the data returned from describe instances in EC2.
        remoteCmd   A string containing the remote command(s) and all it's parameters.  Consider echoing
                    something as last command.
        action      A string describing the action.  This is only used in printing messages to the console.

    Examples:
        ExecuteRemoteCommand(instance, 'aws s3 sync s3://swcmain/cs6200hw2/ hw2 --delete; echo "DONE"', 'Sync Copy HW2 from S3');

        var remoteCmd = 'cd hw2; node HDS/bin/www ' + instanceConfiguration.hdsStartParameters[i] + nsArgs;
        ExecuteRemoteCommand(instance, remoteCmd, 'Run HDS ' + i.toString());
 */

var ChildProcess = require('child_process');
var instances = require('./Instances.js');

module.exports = function ExecuteRemoteCommand(instanceIndex, remoteCmd, action)
{
    var instance = instances.GetInstance(instanceIndex);
    var args = [ '-o',
        'StrictHostKeyChecking=no',
        '-i',
        '/Users/Life/.ssh/cs7930.pem bitnami@',
        'bitnami@' + instance.publicDns,
        remoteCmd];

    console.log('Start ' + action + ' on ' + instance.instanceId + ' at ' + instance.publicDns);
    var child = ChildProcess.spawn('ssh', args);
    var childOutput = '';

    child.stderr.on('data', function (data) {
        var tmp = data.toString();
        console.log('On ' + instance.instanceId + ', stderr: ' + tmp);
    });

    child.on('close', function (code) {
        if (code !== 0)
            console.log('On ' + instance.instanceId + ', ' + action + ' closed with code ' + code);
        else
            console.log('On ' + instance.instanceId + ', ' + action + ' successful');
        child.stdin.end();
    });

    child.on('exit', function(code, signal) {
        console.log('On ' + instance.instanceId + ', ' + action + ' exited with code ' + code);
    });

    child.on('error', function(err) {
        console.log('On ' + instance.instanceId + ', ' + action + ' ended with error: ' + err.message);
    });
}
