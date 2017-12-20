/**
 * Created by Laleh on 1/9/2015.
 */

var http = require('http');
http.createServer(function (req, res) {
    var sum = 0;
    res.writeHead(200, {'Content-Type': 'text/plain'});

    var message = 'Numbers: ';
    res.write(message);
    console.log(message);
    for(var i = 2; i < process.argv.length; i++)
    {
        var number = process.argv[i];
        message = number + ' ';
        res.write(message);
        console.log(message);
        sum += Number(number);
    }
    message = '\nThe total is ' + sum + '\n';
    res.end(message);
    console.log(message);

}).listen(1337, '127.0.0.1');
console.log('Server running at http://127.0.0.1:1337/');