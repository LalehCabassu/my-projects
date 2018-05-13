/**
 * Created by Life on 2/12/15.
 */

var instances = require('./Instances.js');
var ExecuteRemoteCommand = require('./ExecuteRemoteCommand.js');

var ns_publicDns = '127.0.0.1';

var nsConfig = 'node NS/bin/www 2499 \&';

var doaConfigs = ['node DOA/analyzer 2411 12 1 200 40000 ' + ns_publicDns + ' 2499 \&',
'node DOA/analyzer 2412 13 2 200 40000 ' + ns_publicDns + ' 2499 \&',
'node DOA/analyzer 2413 14 3 200 40000 ' + ns_publicDns + ' 2499 \&'];

var hdsConfigs = ['node HDS/bin/www 2401 9 40000 ' + ns_publicDns + ' 2499 \&',
'HDS/bin/www 2402 10 40000 ' + ns_publicDns + ' 2499 \&',
'HDS/bin/www 2403 11 40000 ' + ns_publicDns + ' 2499 \&'];

var emrConfigs = ['node EMR/Simuilator.js 2421 0 hds9 2 30 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2421 1 hds9 1 40 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2422 2 hds10 2 40 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2422 3 hds10 3 50 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2422 4 hds10 4 60 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2422 5 hds10 1 50 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2423 6 hds11 1 10 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2423 7 hds11 2 10 25  30000 ' + ns_publicDns + ' 2499 &',
'node EMR/Simuilator.js 2423 8 hds11 4 10 25  30000 ' + ns_publicDns + ' 2499 &'];

module.exports =
{
    Do: function()
    {
        ns_publicDns = instances.GetInstance(0).publicDns;
        runProcesses();
    }
}


function runProcesses() {
    console.log('Running the processes on all instances...');

    var configs = ['node NS/bin/www 2499 \&',
        'node hw2/DOA/analyzer 2411 12 1 200 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/DOA/analyzer 2412 13 2 200 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/DOA/analyzer 2413 14 3 200 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/HDS/bin/www 2401 9 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/HDS/bin/www 2402 10 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/HDS/bin/www 2403 11 40000 ' + ns_publicDns + ' 2499 \&',
        'node hw2/EMR/Simuilator.js 2421 0 hds9 2 30 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2421 1 hds9 1 40 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2422 2 hds10 2 40 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2422 3 hds10 3 50 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2422 4 hds10 4 60 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2422 5 hds10 1 50 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2423 6 hds11 1 10 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2423 7 hds11 2 10 25  30000 ' + ns_publicDns + ' 2499 &',
        'node hw2/EMR/Simuilator.js 2423 8 hds11 4 10 25  30000 ' + ns_publicDns + ' 2499 &'
    ];

    var numOfInstances = instance.GetLength();
    for (var i = 0; i < configs.GetLength(); i++)
    {
        var instanceIndex = i % numOfInstances;
        var instance = instances.GetInstance(instanceIndex);
        var remoteCmd = configs[i];
        var message = '';

        switch (i) {
            case 0:
                message = 'Run NS';
                break;
            case 1:
            case 2:
            case 3:
                message = 'Run DOA';
                break;
            case 4:
            case 5:
            case 6:
                message = 'Run HDS';
                break;
            default :
                message = 'Run EMR';
                break;
        }
        message += ' (instances[ ' + i + '])';
        ExecuteRemoteCommand(instance, remoteCmd, message);
    }
}