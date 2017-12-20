/**
 * Created by Life on 3/18/15.
 */

var nameArray = require('./../NameArray.js');
var ClientMessage = require('./../ClientMessage.js');
var RegisterMessage = require('./../RegisterMessage.js');

var express = require('express');
var router = express.Router();
var querystring = require('querystring');

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a neighbor message.");

    var json = req.body.clientMessage;
    var data = ClientMessage.fromJSON(json);
    var neighbor = nameArray.GetNeighbor(data.processId);
    console.log('Process ' + data.processId + '-> Neighbor: {Host: ' + neighbor.host + ', Port: ' + neighbor.port + '}');

    var message = new RegisterMessage(neighbor.host, neighbor.port);
    var response = message.toJSON();

    res.send(response);
});

module.exports = router;

