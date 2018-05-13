/**
 * Created by Life on 4/3/15.
 */

var nameRegistry = require('./../NameRegistry.js');
var ClientMessage = require('./../ClientMessage.js');
var RegisterMessage = require('./../RegisterMessage.js');

var express = require('express');
var querystring = require('querystring');

var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a query resource manager message.");

    var json = req.body.clientMessage;
    var data = ClientMessage.fromJSON(json);
    var rec = nameRegistry.GetName(data.name);

    var response;
    if(rec != null)
    {
        console.log('Process ' + data.name + '-> Query: {Host: ' + rec.host + ', Port: ' + rec.port + '}');

        var message = new RegisterMessage(data.name, rec.host, rec.port);
        response = message.toJSON();
    }
    else
    {
        console.log('Process ' + data.name + '-> Query: Not found!');
        response = false;
    }
    res.send(response);
});

module.exports = router;

