var dgram = require("dgram");
var tsLogger = require('./../lib/TimestampLogger.js');

var Diseases = require('./../lib/Diseases.js');
var DiseaseCounts = require('./../lib/DiseaseCount.js');
var VectorTimestamp = require('./../lib/VectorTimestamp.js');
var endPoints = require('./../lib/EndPoints.js');
var HdsData = require('./HdsData.js');

// Constructor
function NotificationGenerator(rate)
{
    this.rate = rate;
    this.running = false;
}

// post is a callback passed by Simulator, called PostCode
NotificationGenerator.prototype.Run = function()
{
    this.running = true;
    /*
     To schedule the repeated execution of callback every delay milliseconds.
     Returns a intervalObject for possible use with clearInterval().
     Optionally you can also pass arguments to the callback.
     */
    this.rateTimer = setInterval(SendDeltaToDoa, this.rate, this);
    this.rateTimer.generator = this;
}

NotificationGenerator.prototype.Stop = function()
{
    if (this.running)
    {
        clearInterval(this.rateTimer);
        this.running = false;
    }
}

var SendDeltaToDoa = function () {
    var dc;
    var district = process.myProcessId % 9;
    var host;
    var port;
    var deltaCount = HdsData.GetDeltaCounts();

    // missed
    var timestamp = VectorTimestamp.LocalTimestamp();
    timestamp.Increment(process.myProcessId);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    for(var i = 0; i < deltaCount.length; i++) {
        switch (i) {
            case Diseases.FLU:
                host = endPoints.Doa[0].host;
                port = endPoints.Doa[0].port;
                dc = new DiseaseCounts(district, Diseases.FLU, deltaCount[i], timestamp);
                SendMessage(dc, port, host);
                break;
            case Diseases.CHICKEN_POX:
                host = endPoints.Doa[1].host;
                port = endPoints.Doa[1].port;
                dc = new DiseaseCounts(district, Diseases.CHICKEN_POX, deltaCount[i], timestamp);
                SendMessage(dc, port, host);
                break;
            case Diseases.MEASLES:
                host = endPoints.Doa[2].host;
                port = endPoints.Doa[2].port;
                dc = new DiseaseCounts(district, Diseases.MEASLES, deltaCount[i], timestamp);
                SendMessage(dc, port, host);
                break;
            default :
                // do nothing ?!!
                break;
        }
    }
}

var SendMessage = function(dc, port, host)
{
    var json = dc.toJSON();
    var message = new Buffer(json);

    var client = dgram.createSocket("udp4");
    client.send(message, 0, message.length, port, host, function(err, bytes) {
        client.close();
    });
    console.log('\nHDS' + process.myProcessId + ' sent a delta count to DOA listening at port ' + port );
}

module.exports = NotificationGenerator;
