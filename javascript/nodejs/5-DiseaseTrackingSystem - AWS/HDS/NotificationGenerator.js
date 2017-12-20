var dgram = require("dgram");
var tsLogger = require('./../lib/TimestampLogger.js');

var Diseases = require('./../lib/Diseases.js');
var DiseaseCounts = require('./../lib/DiseaseCount.js');
var VectorTimestamp = require('./../lib/VectorTimestamp.js');
var HdsData = require('./HdsData.js');
var NameServerClient = require('../NS/NameServerClient.js');

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
    var district = process.myProcessId % 9;
    var deltaCount = HdsData.GetDeltaCounts();

    // missed
    var timestamp = VectorTimestamp.LocalTimestamp();
    timestamp.Increment(process.myProcessId);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    for(var i = 0; i < deltaCount.length; i++) {
        var doa_name, disease;
        switch (i) {
            case Diseases.FLU:
                doa_name = "doa" + 12;
                disease = Diseases.FLU;
                break;
            case Diseases.CHICKEN_POX:
                doa_name = "doa" + 13;
                disease = Diseases.CHICKEN_POX;
                break;
            case Diseases.MEASLES:
                doa_name = "doa" + 14;
                disease = Diseases.MEASLES;
                break;
            default :
                // do nothing ?!!
                break;
        }
        NameServerClient.Lookup(doa_name, function (error, data) {
            if(error)
                console.log("Error in looking up the "+ doa_name + " address")
            else if(data.name === doa_name && data.type === 'doa') {
                var host = data.host;
                var port = data.port;
                var dc = new DiseaseCounts(district, disease, deltaCount[i], timestamp);
                SendMessage(dc, port, host);
            }
        });
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
