/**
 * Created by Life on 4/18/15.
 */

var GeneralMessage = require('./../../lib/GeneralMessage.js');
var ResourceManager = require('./../ResourceManager.js');
var webservice = require('./../../lib/Webservice.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Unlock message.");

    var json = req.body.generalMessage;
    var data = GeneralMessage.fromJSON(json);

    ResourceManager.Unlock(data.tranId, function(err, response){
        console.log("Unlocked (tranId: " + data.tranId + ")");
        res.send(response);
    });
});

module.exports = router;
