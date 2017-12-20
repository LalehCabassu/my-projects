/**
 * Created by Life on 4/16/15.
 */

var ResourceManagerRegistry = require('./../ResourceManagerRegistry.js');
var ResourceManagerMessage = require('./../ResourceManagerMessage.js');

var querystring = require('querystring');
var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a query Resource Manager Message.");

    var json = req.body.resourceManagerMessage;
    var data = ResourceManagerMessage.fromJSON(json);
    ResourceManagerRegistry.Add(data.resource, data.resourceManager);

    var rec = ResourceManagerRegistry.Get(data.resource);
    console.log('Registered resource (' + data.resource + ') => Resource Manager: ' + rec);

    res.send(true);
});

module.exports = router;
