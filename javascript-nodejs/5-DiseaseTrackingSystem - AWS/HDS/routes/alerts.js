var express = require('express');
var OutbreakAlert = require('./../../lib/OutbreakAlert.js');
var tsLogger = require('./../../lib/TimestampLogger.js');
var VectorTimestamp = require('./../../lib/VectorTimestamp.js');
var diseases = require('./../../lib/Diseases.js');

var router = express.Router();

router.post('/', function(req, res) {
    console.log('Got an OUTBREAK ALERT !!!');
    var json = req.body.alert;
    var a = OutbreakAlert.fromJSON(json);

    // on receive
    var timestamp = VectorTimestamp.LocalTimestamp();
    timestamp.Increment(process.myProcessId);
    timestamp.Merge(a.timestamp);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    console.log('OUTBREAK ALERT from ' +
    a.district.toString() +
    ' for ' +
    diseases.DISEASE_NAMES[a.disease.toString()]);

   // missed
    timestamp.Increment(process.myProcessId);
    tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

    var json = timestamp.toJSON();

    //res.writeHead(200, { 'content-type': 'text/plain' })
    res.send(json);
});

/* GET users listing. */
router.get('/', function(req, res) {
});

module.exports = router;
