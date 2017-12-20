/**
 * Created by Life on 3/26/15.
 */

var ResourceManagerRegistry = require('./../ResourceManagerRegistry.js');
var RegisterMessage = require('./../../NS/RegisterMessage.js');

var querystring = require('querystring');
var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Register Message.");

    var json = req.body.registerMessage;
    var data = RegisterMessage.fromJSON(json);
    ResourceManagerRegistry.Add(data.name, data.host, data.port);

    var rec = ResourceManagerRegistry.Get(data.name);
    console.log('Registered {name: ' + data.name + ' ' + rec.toString() + '}');
    res.send(true);
});

module.exports = router;
