var querystring = require('querystring');
var http = require('http');
var BufferList = require('bl');
var Notification = require('../lib/Notification.js');
var NotificationGenerator = require('./NotificationGenerator.js');
var diseases = require('../lib/Diseases.js');
var diseaseCounts = require('./DiseaseCounts.js');
// TODO: Require your VectorTimestamp module
var VectorTimestamp = require('./../lib/VectorTimestamp.js');

// TODO: require your timestamp logger
var tsLogger = require('./../lib/TimestampLogger.js');

var dhsServer = '';
var dhsPort = '';
var generator = '';
process.myProcessId = -1;

// TODO: Setup local vector timestamp
// create a new instance or get it from notification??
// initialize to zero????
VectorTimestamp.Init(15);
var timestamp = VectorTimestamp.LocalTimestamp();

function ProcessResponse(response)
{
    var data = new BufferList();
    response.setEncoding('utf8');

    response.on("data", function(chunk) {
        data.append(chunk);
    });

    response.on("end", function() {
        // TODO: using the data as a json string, create a vector timestamp object
        var dataStr = data.toString();
        var newTS = VectorTimestamp.fromJSON(dataStr);

        // TODO: update local timestamp
        // on receive
        // localTS.Increment() + localTS.merge(newTS) ???
        timestamp.Increment(process.myProcessId);
        timestamp.Merge(newTS);
        tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);
    });
}

function PostCode(notification) {
    // on send
    timestamp.Increment(process.myProcessId);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);
    notification.timestamp = timestamp;

    // Build the post string from an object
    var json = notification.toJSON();
    var post_data = querystring.stringify({ notification: json} );

    // An object of options to indicate where to post to
    var post_options = {
        host: dhsServer,
        port: dhsPort,
        path: '/notify',
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': post_data.length
        }
    };

    // Set up the request
    var post_req = http.request(post_options, ProcessResponse);
    post_req.on('error', function(e) {
        console.log('Process ' + process.myProcessId + '-> Error posting request to server: ' + e.message);
    });

    // post the data
    post_req.write(post_data);
    post_req.end();
}

console.log(process.argv);

if (process.argv.length>=8)
{
    // argv[0 - 4] are
    // process id, timestamp log file, server address, server port, ???
    // TODO: Setup local process id
    process.myProcessId = Number(process.argv[2]);

    // TODO: Setup up timestamp log
    tsLogger.SetupLogFile(process.myProcessId);

    // TODO: Log initial timestamp
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    // TODO: Setup server address
    dhsServer = process.argv[3];

    // TODO: Setup server port
    dhsPort = process.argv[4];

    // TODO: Change the input argument numbers according to your own design
    // arguments: startRate, maxRate, rateChange
    generator = new NotificationGenerator(parseInt(process.argv[5]), parseInt(process.argv[6]), parseInt(process.argv[7]));
    generator.Run(PostCode);

    // Setup a reoccurring timer to display current totals every second
    displayTimer = setInterval(
                    function () {
                        console.log('');
                        console.log('At ' + new Date);
                        // TODO: write out the current local timestamp
                        console.log('TS: ' + timestamp.ToString());

                        //  TODO: write out the disease counts
                        // where is the disease counts?
                        console.log('DC:' + diseaseCounts.GetCounts());

                    }, 1000);

    // Setup a timer to stop everything a specified number of ms from now
    setTimeout(function() { generator.Stop(); clearInterval(displayTimer); }, parseInt(process.argv[8]));

}
else
{
    console.log("No Server URL provided");
}

//process.on('uncaughtException', function (err) {
//    console.log(err);
//});
