/**
 * Created by Life on 4/4/15.
 */

var GeneralMessage = require('./../../lib/GeneralMessage.js');
var ResourceManager = require('./../ResourceManager.js');
var webservice = require('./../../lib/Webservice.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a ToLower message.");

    var json = req.body.generalMessage;
    var data = GeneralMessage.fromJSON(json);

    ResourceManager.ToLower(data.tranId, function(err, response){
        console.log("ToLowered (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;
