/**
 * Created by Life on 2/4/15.
 */

var express = require('express');
var router = express.Router();
var nameArray = require('./../NameArray.js');
var querystring = require('querystring');

/* POST home page. */
router.post('/', function(req, res) {
    var data = req.body;
    nameArray.AddRecord(data.name, data.type, data.host, data.port);

    var rec = nameArray.GetRecord(data.name);
    console.log('Registered {Name: ' + data.name + ' ' + rec.toString());

    res.send(true);
});

module.exports = router;
