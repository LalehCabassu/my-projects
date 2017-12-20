/**
 * Created by Life on 4/3/15.
 */
var nameArray = require('./../NameArray.js');
var ClientMessage = require('./../ClientMessage.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a deregister message.");

    var json = req.body.clientMessage;
    var data = ClientMessage.fromJSON(json);
    nameArray.RemoveNode(data.processId);
    console.log('Process ' + data.processId + '-> Deregistered.');

    res.send(true);
});

module.exports = router;

