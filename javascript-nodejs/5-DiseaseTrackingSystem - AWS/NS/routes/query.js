/**
 * Created by Life on 2/4/15.
 */

var express = require('express');
var router = express.Router();
var nameArray = require('./../NameArray.js');
var querystring = require('querystring');

/* POST home page. */
router.post('/', function(req, res) {
    var name = req.body.name;
    var data = nameArray.GetRecord(name);
    console.log('Looked up {Name: ' +
    name + ', Type: ' + data.type + ', Host: ' + data.host + ', Port: ' + data.port + '}');

    var res_data = querystring.stringify({name: name, type: data.type, host: data.host, port: data.port});
    res.send(res_data);
});

module.exports = router;
