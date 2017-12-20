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

module.exports = function ExecuteRemoteCommand(instance, remoteCmd, action)
{
    var args = [ '-o',
        'StrictHostKeyChecking=no',
        '-i',
        '/Users/swc/.ssh/AWS.east.stephen.clyde@usu.edu.pem',
        'bitnami@' + instance.PublicDnsName,
        remoteCmd];

    console.log('Start ' + action + ' on ' + instance.InstanceId + ' at ' + instance.PublicDnsName);
    var child = ChildProcess.spawn('ssh', args);
    var childOutput = '';

    child.stderr.on('data', function (data) {
        var tmp = data.toString();
        console.log('On ' + instance.InstanceId + ', stderr: ' + tmp);
    });

    child.on('close', function (code) {
        if (code !== 0)
            console.log('On ' + instance.InstanceId + ', ' + action + ' closed with code ' + code);
        else
            console.log('On ' + instance.InstanceId + ', ' + action + ' successful');
        child.stdin.end();
        isDone = true;
    });

    child.on('exit', function(code, signal) {
        console.log('On ' + instance.InstanceId + ', ' + action + ' exited with code ' + code);
    });

    child.on('error', function(err) {
        console.log('On ' + instance.InstanceId + ', ' + action + ' ended with error: ' + err.message);
    });
}
