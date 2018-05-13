/**
 * Created by Life on 2/6/15.
 */

var querystring = require('querystring');
var http = require('http');
var BufferList = require('bl');

var nshost, nsport, res_done = false;

module.exports  =
{
    Setup: function(host, port)
    {
        nshost = host;
        nsport = port;
    },

    Register: function(name, type, host, port, callback)
    {
        res_done = false;
        var post_data = querystring.stringify({name: name, type: type, host: host, port: port});
        CallWebService('register', post_data, callback, 10);
    },

    Lookup: function(name, callback)
    {
        res_done = false;
        var post_data = querystring.stringify({name: name});
        CallWebService('query', post_data, callback, 10);
    }
}

function CallWebService(action, post_data, callback, trycount)
{
    var post_options = {
        host: nshost,
        port: nsport,
        path: '/' + action,
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': post_data.length
        }
    };

    // Set up the request
    var post_req = http.request(post_options,function(res)
    {
        var data = new BufferList();
        res.setEncoding('utf8');

        res.on("data", function(chunk) {
            data.append(chunk);
        });

        res.on("end", function() {
            var data_str = data.toString();
            if(data_str != '' && data_str != undefined)
            {
                res_done = true;
                if(data_str === "true") {
                    callback(null, true);
                }
                else
                {
                    var res_data = querystring.parse(data_str);
                    callback(null, res_data);
                }
            }
            else
                setTimeout(CallWebService, 100);
        });
    });

    // Setup the error handler
    post_req.on('error', function(e) {
        console.log('Error posting request to server: ' + e.message);
        while(!res_done && (trycount--) > 0)
            setTimeout(CallWebService, 100);
    });

    // post the data
    post_req.write(post_data);
    post_req.end();
}