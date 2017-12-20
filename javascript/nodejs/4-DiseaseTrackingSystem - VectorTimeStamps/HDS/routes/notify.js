var express = require('express');

var VectorTimestamp = require('./../../lib/VectorTimestamp.js');
var Notification = require('./../../lib/Notification.js');
var diseases = require('./../../lib/Diseases.js');
var HdsData = require('./../HdsData.js');
var tsLogger = require('./../../lib/TimestampLogger.js');


var router = express.Router();

/* Post a new disease notification. */
router.post('/', function(req, res) {
    var json = req.body.notification;
    var n = Notification.fromJSON(json);

    // on receive
    var timestamp = VectorTimestamp.LocalTimestamp();
    timestamp.Increment(process.myProcessId);
    timestamp.Merge(n.timestamp);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    HdsData.Record(n);

    console.log('Received and posted a disease notification from ' +
                n.emr.toString() +
                ' for ' +
                diseases.DISEASE_NAMES[n.disease.toString()]);

    // missed
    timestamp.Increment(process.myProcessId);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);
    var json = timestamp.toJSON();

    //res.writeHead(200, { 'content-type': 'text/plain' })
    res.send(json);
   // res.end();
});

/* Get a copy of the last notification */
router.get('/', function(req, res) {
    res.send('Get the last disease notification');
});


module.exports = router;

