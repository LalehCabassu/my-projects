var fs = require('fs');
var stringUtil = require('./StringUtil');

var logfiles = ['Timestamp.log',
    'emr00.log', 'emr01.log', 'emr02.log',
    'emr03.log', 'emr04.log', 'emr05.log',
    'emr06.log', 'emr07.log', 'emr08.log',
    'hds09.log', 'hds10.log', 'hds11.log',
    'doa12.log', 'doa13.log', 'doa14.log'];

var myFunctions = {
    // Provide a method to setup the log file
    SetupLogFile : function(processId)
    {
        fs.closeSync(fs.openSync(logfiles[0], 'w', function(err, fd){
                if (err)    throw err;
                console.log('Setup the log file: ' + logfiles[0]);
            })
        );

        fs.closeSync(fs.openSync(logfiles[processId + 1], 'w', function(err, fd){
                if (err)    throw err;
                console.log('Setup the log file: ' + logfiles[processId]);
            })
        );

    },

    LogProcessTimestamp : function(processId, timestamp)
    {
        var msg = 'Process ' + stringUtil.NumberPadder(processId, 3) + ': [' + stringUtil.GetDateTime() + ']: '
            + timestamp.ToString() + '\n';
        var logFileIndex = processId + 1;
        myFunctions.LogFileIndexString(logFileIndex, msg);
        myFunctions.LogString(msg);
    },

    // Provide a function to log a timestamp
    LogTimestamp : function(timestamp)
    {
        myFunctions.LogString(timestamp.ToString());
    },

    // Provide a function to log a string
    LogFileIndexString : function(fileIndex, text)
    {
        fs.appendFile(logfiles[fileIndex], text, function (err) {
            if (err) throw err;
        });
    },

    LogString : function(text)
    {
        fs.appendFile(logfiles[0], text, function (err) {
            if (err) throw err;
        });
    }
}

module.exports = myFunctions;