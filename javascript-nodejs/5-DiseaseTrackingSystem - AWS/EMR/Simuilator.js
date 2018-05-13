var querystring = require('querystring');
var http = require('http');
var BufferList = require('bl');
var NotificationGenerator = require('./NotificationGenerator.js');
var diseaseCounts = require('./DiseaseCounts.js');
var Notification = require('../lib/Notification.js');
// TODO: Require your VectorTimestamp module
var VectorTimestamp = require('../lib/VectorTimestamp.js');

// TODO: require your timestamp logger
var tsLogger = require('../lib/TimestampLogger.js');

var NetworkInterfaces = require('../lib/NetworkInterfaces.js');
var NameServerClient = require('../NS/NameServerClient.js');

var port = parseInt(process.argv[2]) || 2400;
var hds_name, generator;

// TODO: Setup local vector timestamp
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

    NameServerClient.Lookup(hds_name, function (error, data) {
        if(error)
            console.log("Error in looking up the "+ hds_name + " address")
        else if(data.name === hds_name && data.type === 'hds') {
            var hds_host = data.host;
            var hds_port = data.port;

            // An object of options to indicate where to post to
            var post_options = {
                host: hds_host,
                port: hds_port,
                path: '/notify',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Content-Length': post_data.length
                }
            };

            // Set up the request
            var post_req = http.request(post_options, ProcessResponse);
            post_req.on('error', function (e) {
                console.log('Process ' + process.myProcessId + '-> Error posting request to server: ' + e.message);
            });

            // post the data
            post_req.write(post_data);
            post_req.end();
        }
    });
}

function main() {
    if (process.argv.length >= 11) {
        // TODO: Setup local process id
        process.myProcessId = Number(process.argv[3]);
        process.myName = 'emr' + process.myProcessId.toString();

        // Setup name server endpoint
        var nshost = process.argv[9];
        var nsport = parseInt(process.argv[10]);
        NameServerClient.Setup(nshost, nsport);

        // Register to the name server
        var publicAddress = NetworkInterfaces.GetBestPublicAddress();
        NameServerClient.Register(process.myName, 'emr', publicAddress, port, function (error, data) {
            if (error != null)
                console.log('Error in registering ' + process.myName + ' with the Name Server, error=' + error);
            else
                console.log('Registered ' + process.myName + ' as ' + publicAddress + ':' + port);
        });

        // TODO: Setup up timestamp log
        tsLogger.SetupLogFile(process.myProcessId);

        // TODO: Log initial timestamp
        tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

        // TODO: Setup server address
        hds_name = process.argv[4];

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
        setTimeout(function () {
            generator.Stop();
            clearInterval(displayTimer);
        }, parseInt(process.argv[8]));

    }
    else
        console.log("No Server URL provided");
}

http.createServer().listen(port, '127.0.0.1');

main();