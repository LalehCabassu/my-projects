/**
 * Created by Life on 3/26/15.
 */

var ClientMessage = require('./../../NS/ClientMessage.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    //console.log("Got an AmAlive message.");

    var json = req.body.clientMessage;
    var data = ClientMessage.fromJSON(json);

    var message = new ClientMessage(process.processId);
    var response = message.toJSON();
    //var response = querystring.stringify({ clientMessage: json} );

    res.send(response);
});

module.exports = router;
