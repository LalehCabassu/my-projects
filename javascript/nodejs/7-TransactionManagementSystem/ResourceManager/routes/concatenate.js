/**
 * Created by Life on 4/18/15.
 */

var ConcatenateMessage = require('./../ConcatenateMessage.js');
var ResourceManager = require('./../ResourceManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Concatenate message.");

    var json = req.body.concatenateMessage;
    var data = ConcatenateMessage.fromJSON(json);

    ResourceManager.Concatenate(data.tranId, data.value1, data.value2, function(err, response){
        console.log("Concatenated (tranId: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;
