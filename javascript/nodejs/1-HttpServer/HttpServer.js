var http = require('http');
http.createServer(function(req, res) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write('<h1>Hello World</h1>\n');
    res.write('bye');
    res.end();
    console.log('Response handle');
}).listen(1337, '127.0.0.1');
console.log('Server running at http://127.0.0.1:1337/');



