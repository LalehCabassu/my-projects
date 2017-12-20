/**
 * Created by Life on 4/4/15.
 */

var resourceManager = require('./../ResourceManager.js');
var dataRecord = require('./../DataRecord.js');

var express = require('express');
var router = express.Router();

/* POST home page. */
router.post('/', function(req, res) {
    console.log("Got an SaveResult message:");

    var json = req.body.saveResult;
    var data = dataRecord.fromJSON(json);

    var response = false;
    resourceManager.SaveResult(data, function(err, data){
        if(err){
            console.log("Error in saving result. Error: ", err);
        }
        else
        {
            console.log("Saved result: ", data);
            response = true;
        }
        res.send(response);
    });

    //res.send(response);
});

module.exports = router;
