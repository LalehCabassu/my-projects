/**
 * Created by Life on 4/18/15.
 */

var WriteMessage = require('./../WriteMessage.js');
var ResourceManager = require('./../ResourceManager.js');
var webservice = require('./../../lib/Webservice.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Write message.");

    var json = req.body.writeMessage;
    var data = WriteMessage.fromJSON(json);

    ResourceManager.Write(data.tranId, data.newValue, function(err, response){
        console.log("Wrote (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;
