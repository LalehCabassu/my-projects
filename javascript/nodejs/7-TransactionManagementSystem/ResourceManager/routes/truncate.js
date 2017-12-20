/**
 * Created by Life on 4/18/15.
 */

var TruncateMessage = require('./../TruncateMessage.js');
var ResourceManager = require('./../ResourceManager.js');
var webservice = require('./../../lib/Webservice.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a TruncateMessage message.");

    var json = req.body.truncateMessage;
    var data = TruncateMessage.fromJSON(json);

    ResourceManager.Truncate(data.tranId, data.index, function(err, response){
        console.log("Truncated (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;