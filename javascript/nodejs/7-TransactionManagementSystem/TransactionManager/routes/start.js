/**
 * Created by Life on 4/8/15.
 */

var GeneralMessage = require('./../../lib/GeneralMessage.js');
var TransactionManager = require('./../TransactionManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got an Start message.");

    var tranId = TransactionManager.Start();
    console.log("Transaction created. (Id: " + tranId + ")");
    var response = JSON.stringify(tranId);
    res.send(response);
});

module.exports = router;
