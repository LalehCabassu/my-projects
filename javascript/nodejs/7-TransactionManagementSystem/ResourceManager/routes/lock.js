/**
 * Created by Life on 4/8/15.
 */


var LockMessage = require('./../LockMessage.js');
var ResourceManager = require('./../ResourceManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Lock message.");

    var json = req.body.lockMessage;
    var data = LockMessage.fromJSON(json);

    ResourceManager.Lock(data.tranId, data.type, data.timeout, function(err, response){
        console.log("Locked-" + data.type + " (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;