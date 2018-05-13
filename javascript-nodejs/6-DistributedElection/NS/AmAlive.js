//var AmAliveGenerator = require('./AmAliveGenerator.js');
var nameArray = require('./NameArray.js');
var webservice = require('./../lib/Webservice.js');
var clientMessage = require('./ClientMessage.js');

var querystring = require('querystring');

function sendAmAlive() {

    // send amalive messages
    for (var i = 0; i < nameArray.GetLength(); i++) {
        var message = new clientMessage(i);
        var json = message.toJSON();
        var post_data = querystring.stringify({clientMessage: json});

        var node = nameArray.GetNode(i);
        if(node != null) {
            node.aliveness--;
            webservice(node.host, node.port, '/amalive', post_data, function (error, data) {
                if (error) {
                    // Deregister the node.
                    //nameArray.RemoveNode(processId);
                    //console.log('Process Id: ' + processId + ' is not alive.');
                }
                else
                {
                    var processId = data.processId;
                    var node = nameArray.GetNode(processId);
                    if(node != null)
                        node.aliveness++;
                    console.log('Process Id: ' + processId + ' is alive.');
                }
            }, 10, 10);
            console.log('Sent an AmAlive msg to Process Id: ' + i);
        }
    }
}

function removeDeadNodes(){
    for(var i = 0; i < nameArray.GetLength(); i++) {
        var node = nameArray.GetNode(i);
        //console.log('*** Checking aliveness of Process Id: ' + i);
        if (node != null && !node.isAlive()) {
            //nameArray.RemoveNode(i);
            console.log('*** Process Id: ' + i + ' is not alive.');
        }
    }
}

function RunGenerator(rate, timeout) {
    //var generator = new AmAliveGenerator(startRate, maxRate, rateChange);
    //generator.Run(PostCode);
    var amAliveInterval = setInterval(sendAmAlive, rate)
    var removeDeadNodesInterval = setInterval(removeDeadNodes, rate * 100);

    // Setup a timer to stop everything a specified number of ms from now
    setTimeout(function () {
        //generator.Stop();
        clearInterval(amAliveInterval);
        clearInterval(removeDeadNodesInterval);
    }, parseInt(timeout));
 }

module.exports = RunGenerator;