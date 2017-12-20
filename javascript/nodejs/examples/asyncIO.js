/**
 * Created by Laleh on 1/11/2015.
 */

var http = require('http');
var fs = require('fs');

http.createServer(function (req, res) {
    res.writeHead(200, {'Content-Type': 'text/plain'});

    res.write('\nReading the file at ' + process.argv[2]);
    fs.readFile(process.argv[2], 'utf8', function (err, data)
    {
        if(err)
            throw err;
        res.end('\nFile content:\n' + data.toString());
        console.log('Finished reading the file');
    });
}).listen(1337, '127.0.0.1');
console.log('Server running at http://127.0.0.1:1337/');