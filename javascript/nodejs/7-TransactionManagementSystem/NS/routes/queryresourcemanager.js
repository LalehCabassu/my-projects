/**
 * Created by Life on 4/16/15.
 */

var resourceManagerRegistry = require('./../ResourceManagerRegistry.js');
var ClientMessage = require('./../ClientMessage.js');
var ResourceManagerMessage = require('./../ResourceManagerMessage.js');
var nameRegistry = require('./../NameRegistry.js');

var express = require('express');
var querystring = require('querystring');

var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a query resource manager message.");

    var json = req.body.clientMessage;
    var data = ClientMessage.fromJSON(json);
    var resourceManager = resourceManagerRegistry.Get(data.name);
    var rec = nameRegistry.GetName(resourceManager);

    var response;
    if(resourceManager != null && rec != null)
    {
        console.log('Resource ' + data.name + '-> Resource manager: ' + resourceManager);

        var message = new ResourceManagerMessage(data.name, resourceManager, rec.host, rec.port);
        response = message.toJSON();
    }
    else
    {
        console.log('Resource ' + data.name + '-> Query: Not found!');
        response = false;
    }
    res.send(response);
});

module.exports = router;

