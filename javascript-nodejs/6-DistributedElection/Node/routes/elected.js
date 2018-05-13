/**
 * Created by Life on 4/8/15.
 */


var ElectionMessage = require('./../ElectionMessage.js');
var ElectionManager = require('./../ElectionManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got an Elected message.");

    var json = req.body.electionMessage;
    var data = ElectionMessage.fromJSON(json);

    ElectionManager.ProcessElectedMessage(data.processId);

    res.send(true);
});

module.exports = router;
