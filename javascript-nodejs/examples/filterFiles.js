/**
 * Created by Laleh on 1/11/2015.
 */

var http = require('http');
var ds = require('./directorySearch.js');

http.createServer(function (req, res) {
    res.writeHead(200, {'Content-Type': 'text/plain'});

    var p = process.argv[2];
    var ext = process.argv[3];

    res.write('\nListing files(' + ext + ') under ' + p);

    ds.listFiles(p, ext, writeResponse, res);

}).listen(1337, '127.0.0.1');

function writeResponse(err, data, res)
{
    for(var i = 0; i < data.length; i++)
    {
        res.write('\n' + i + ': ' + data[i]);
    }
    res.end('\ndone!');
}

console.log('Server running at http://127.0.0.1:1337/');