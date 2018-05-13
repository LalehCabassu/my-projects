/**
 * Created by Life on 4/18/15.
 */

var SubStringMessage = require('./../SubStringMessage.js');
var ResourceManager = require('./../ResourceManager.js');
var webservice = require('./../../lib/Webservice.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a SubStringMessage message.");

    var json = req.body.subStringMessage;
    var data = SubStringMessage.fromJSON(json);

    ResourceManager.SubString(data.tranId, data.startIndex, data.endIndex, function(err, response){
        console.log("SubStringed (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;
