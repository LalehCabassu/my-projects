/**
 * Created by Life on 4/8/15.
 */

var GeneralMessage = require('./../../lib/GeneralMessage.js');
var ResourceManager = require('./../ResourceManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Read message.");

    var json = req.body.generalMessage;
    var data = GeneralMessage.fromJSON(json);

    ResourceManager.Read(data.tranId, function(err, value){
        console.log("Read resource. (Value: " + value + ")");
        var response = JSON.stringify(value);
        res.send(response);
    });
});

module.exports = router;
