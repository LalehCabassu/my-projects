/**
 * Created by Life on 2/4/15.
 */

var nameRegistry = require('./../NameRegistry.js');
var RegisterMessage = require('./../RegisterMessage.js');

var querystring = require('querystring');
var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Register Message.");

    var json = req.body.registerMessage;
    var data = RegisterMessage.fromJSON(json);
    nameRegistry.AddName(data.name, data.host, data.port);

    var rec = nameRegistry.GetName(data.name);
    console.log('Registered {' + rec.toString() + '}');

    res.send(true);
});

module.exports = router;
