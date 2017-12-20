var querystring = require('querystring');

var nameServerClient = require('./../NS/NameServerClient.js');
var worker = require('./Worker.js');

function main() {
    if (process.argv.length >= 9) {
        process.name = process.argv[2];
        var ipAddress = '127.0.0.1';
        var port = parseInt(process.argv[3]) || 2421;

        // Setup name server endpoint
        var nshost = process.argv[4];
        var nsport = parseInt(process.argv[5]);
        nameServerClient.Setup(nshost, nsport);

        // Setup transaction manager endpoint
        var tmhost = process.argv[6];
        var tmport = parseInt(process.argv[7]);

        var path = process.argv[8];

        // Register to the name server
        nameServerClient.Register(process.name, ipAddress, port, function (error, data) {
            if (error != null || data == false)
                console.log('{' + ipAddress + ':' + port + '} -> Failed in registering to the name server! Name: ' + process.name);
            else {
                console.log('{' + ipAddress + ':' + port + '} -> Successfully registered. Name: ' + process.name);
                worker.Active(nshost, nsport, tmhost, tmport);
                worker.Run(path, function(err, data){
                    if(err)
                        console.log("Error: " + err);
                    else
                        console.log("Running transaction succeeded? " + data);
                });
            }
        });
    }
}

main();