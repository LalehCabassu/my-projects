var dgram = require("dgram");
var http = require('http');
var BufferList = require('bl');

var querystring = require('querystring');
var VectorTimestamp = require('../lib/VectorTimestamp.js');
var endPoints = require('../lib/EndPoints.js');
var tsLogger = require('../lib/TimestampLogger.js');
var diseases = require('../lib/Diseases.js');
var DiseaseCount = require('../lib/DiseaseCount.js');
var OutbreakAlert = require('../lib/OutbreakAlert.js');

var myDisease = -1;
var countBuffer = new Array(10);
var currentBufferIndex = 0;
var windowSize = 5;
var diseaseThreshold = 30;
var udpSocket = null;

function ProcessResponse(response)
{
    var data = new BufferList();
    response.setEncoding('utf8');

    response.on("data", function(chunk) {
        data.append(chunk);
    });

    response.on("end", function() {
        console.log('Got response from Alert');
        var json = data.toString();
        var vt = VectorTimestamp.fromJSON(json);
        VectorTimestamp.LocalTimestamp().Increment();
        VectorTimestamp.LocalTimestamp().Merge(vt);
        tsLogger.LogProcessTimestamp(process.myProcessId, VectorTimestamp.LocalTimestamp());
    });
}

function PostCode(alert) {
    console.log('Send Alert to all HDS');
    VectorTimestamp.LocalTimestamp().Increment();
    tsLogger.LogProcessTimestamp(process.myProcessId,VectorTimestamp.LocalTimestamp());
    alert.timestamp = VectorTimestamp.LocalTimestamp();

    // Build the post string from an object
    var json = alert.toJSON();
    var post_data = querystring.stringify({alert: json});

    for (var i = 0; i < endPoints.Hds.length; i++) {
        // An object of options to indicate where to post to
        var post_options = {
            host: endPoints.Hds[i].host,
            port: endPoints.Hds[i].port,
            path: '/alerts',
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Content-Length': post_data.length
            }
        };

        // Set up the request
        var post_req = http.request(post_options, ProcessResponse);

        // Setup the error handler
        post_req.on('error', function(e) {
            console.log('Error posting request to server: ' + e.message);
        });

        // post the data
        post_req.write(post_data);
        post_req.end();
    }
}

function RoleForward()
{
    currentBufferIndex = NextIndex(currentBufferIndex);
    for (i=0; i<endPoints.Hds.length; i++)
        countBuffer[currentBufferIndex][i] = 0;
}

function ProcessMessage(message)
{
    var dc = DiseaseCount.fromJSON(message);

    VectorTimestamp.LocalTimestamp().Increment(process.myProcessId);
    VectorTimestamp.LocalTimestamp().Merge(dc.timestamp);
    tsLogger.LogProcessTimestamp(process.myProcessId, VectorTimestamp.LocalTimestamp());

    if (dc.disease==myDisease)
    {
        console.log('Add ' + dc.delta.toString() + ' to countBuffer[' + currentBufferIndex.toString() + '][' + dc.district.toString() + '](' + countBuffer[currentBufferIndex][dc.district] + ')');
        countBuffer[currentBufferIndex][dc.district] +=  parseInt(dc.delta);
        console.log('countBuffer[' + currentBufferIndex.toString() + '][' + dc.district +']=' + countBuffer[currentBufferIndex][dc.district].toString());
    }
}

function NextIndex(index)
{
    index++;
    if (index>=countBuffer.length) index = 0;
    return index;
}

function CheckDistrictForOutbreak(district)
{
    var startingIndex = currentBufferIndex - (windowSize - 1);
    if (startingIndex<0) startingIndex = countBuffer.length + startingIndex - 1;

    console.log('Compute total for district ' + district.toString());
    var total = 0;
    for (var i=0; i<windowSize; i++)
    {
        console.log('Add in index ' + startingIndex.toString());
        total += countBuffer[startingIndex][district];
        startingIndex = NextIndex(startingIndex);
    }

    console.log('Total for district ' + district.toString() + ' for ' + diseases.DISEASE_NAMES[myDisease] + ' = ' + total.toString());
    if (total>=diseaseThreshold)
    {
        var alert = new OutbreakAlert(district, myDisease);
        PostCode(alert);
    }
}

function CheckForOutbreak()
{
    for (var i=0; i<endPoints.Hds.length; i++)
        CheckDistrictForOutbreak(i);
}

function main() {
    if (process.argv.length >= 7) {

        // Setup the process id
        process.myProcessId = parseInt(process.argv[3]);
        myDisease = parseInt(process.argv[4]);
        diseaseThreshold = parseInt(process.argv[5]);

        // Setup the local timestamp
        VectorTimestamp.Init(15);
        for (var i = 0; i < 10; i++) {
            countBuffer[i] = new Array(endPoints.Hds.length);
            for (var j = 0; j < endPoints.Hds.length; j++)
                countBuffer[i][j] = 0;
        }

        // Setup the logger and write out the initial timestamp
        tsLogger.SetupLogFile(process.myProcessId);
        //tsLogger.LogString('Starting DOA' + process.myProcessId.toString() + '\n');
        tsLogger.LogProcessTimestamp(process.myProcessId, VectorTimestamp.LocalTimestamp());

        // Setup Datagram Socket and Event Handlers
        udpSocket = dgram.createSocket("udp4");

        udpSocket.on("error", function (err) {
            console.log("UDP Socket error:\n" + err.stack);
            udpSocket.close();
        });

        udpSocket.on("message", function (msg, rinfo) {
            console.log("Got: " + msg + " from " + rinfo.address + ":" + rinfo.port);
            ProcessMessage(msg);
        });

        udpSocket.on("listening", function () {
            var address = udpSocket.address();
            console.log("server listening " +
            address.address + ":" + address.port);
        });

        udpSocket.bind(parseInt(process.argv[2], 10));

        // Set up a interval timer for checking whether there is an outbreak
        var checkTimer = setInterval(CheckForOutbreak, 1000);
        var nextTimer = setInterval(RoleForward, 1000);

        // Stop everything after specified time
        setTimeout(function () {
            udpSocket.close();
            clearInterval(checkTimer);
            clearInterval(nextTimer);
        }, parseInt(process.argv[6]));
    }
    else
        console.log('Some parameters are missing!');
}

main();