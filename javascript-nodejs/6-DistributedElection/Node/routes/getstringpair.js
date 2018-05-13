/**
 * Created by Life on 4/3/15.
 */

var resourceManager = require('./../ResourceManager.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got an getStringPair message.");

    var response = false;
    resourceManager.GetStringPair(function(err, data){
        if(err){
            console.log("Error in getting a string pair. Error: ", err);
        }
        else
        {
            console.log("Got a new string pair: ", data);
            response = data.toJSON();
        }
        res.send(response);
    });

   // res.send(response);
});

module.exports = router;
