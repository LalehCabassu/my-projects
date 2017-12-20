/**
 * Created by Life on 4/3/15.
 */

var nameArray = require('./../NameArray.js');
var clientMessage = require('./../ClientMessage.js');
var RegisterMessage = require('./../RegisterMessage.js');

var express = require('express');
var router = express.Router();
var querystring = require('querystring');

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a query message.");

    var json = req.body.clientMessage;
    var data = clientMessage.fromJSON(json);
    var node = nameArray.GetNode(data.processId);

    var response;
    if(node != null && node.isAlive())
    {
        console.log('Process ' + data.processId + '-> Query: {Host: ' + node.host + ', Port: ' + node.port + '}');

        var message = new RegisterMessage(node.host, node.port);
        response = message.toJSON();
    }
    else
    {
        console.log('Process ' + data.processId + '-> Query: Not found!');
        response = false;
    }
    res.send(response);
});

module.exports = router;

