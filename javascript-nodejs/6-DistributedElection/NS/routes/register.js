/**
 * Created by Life on 2/4/15.
 */

var nameArray = require('./../NameArray.js');
var RegisterMessage = require('./../RegisterMessage.js');
var ClientMessage = require('./../ClientMessage.js');

var express = require('express');
var router = express.Router();
var querystring = require('querystring');

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a RegisterMessage.");

    var json = req.body.registerMessage;
    var data = RegisterMessage.fromJSON(json);
    var processId = nameArray.AddNode(data.host, data.port);

    var rec = nameArray.GetNode(processId);
    console.log('Registered {ProcessId: ' + processId + ', ' + rec.toString() + '}');

    var message = new ClientMessage(processId);
    var response = message.toJSON();

    res.send(response);
});

module.exports = router;
