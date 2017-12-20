/**
 * Created by Life on 4/8/15.
 */

var TransactionMessage = require('./../TransactionMessage.js');
var TransactionManager = require('./../TransactionManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got a Commit message.");

    var json = req.body.transactionMessage;
    var data = TransactionMessage.fromJSON(json);

    TransactionManager.Commit(data.tranId, data.resourceManagers, function(err, response){
        console.log("Committed transaction (Id: " + data.tranId + ")? " + response);
        res.send(response);
    });
});

module.exports = router;
