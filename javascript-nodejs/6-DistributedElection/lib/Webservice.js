var http = require('http');
var BufferList = require('bl');

var retryDelay = 1000;

function CallWebservice(host, port, service, post_data, callback, retriesRemaining, retryOnResultFalse) {

    // An object of options to indicate where to post to
    var post_options = {
        host: host,
        port: port,
        path: service,
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': post_data.length
        }
    };

    // Set up the request
    var post_req = http.request(post_options, function(response)
    {
        var data = new BufferList();
        response.setEncoding('utf8');

        response.on("data", function(chunk) {
            data.append(chunk);
        });

        response.on("end", function() {
            var error = null;
            var resultData = false;
            var strData = data.toString();
            if (strData!=null && strData!=undefined && strData!='') {
                try {
                    resultData = JSON.parse(strData);
                }
                catch (e) {
                    error = e;
                    retriesRemaining = 0;
                }
            }
            CallbackOrRetryAgain(error, resultData, host, port, service, post_data, callback, retriesRemaining, retryOnResultFalse);
        });
    });

    // Setup the error handler
    post_req.on('error', function (e)
    {
        CallbackOrRetryAgain(e, null, host, port, service, post_data, callback, retriesRemaining, retryOnResultFalse);
    });

    // post the data
    post_req.write(post_data);
    post_req.end();
}

function CallbackOrRetryAgain(error, resultData, host, port, service, post_data, callback, retryRemaining, retryOnResultFalse)
{
    if (retryRemaining > 0 && ((retryOnResultFalse==true && resultData == false) || error != null))
    {
        retryRemaining--;
        setTimeout(CallWebservice, retryDelay, host, port, service, post_data, callback, retryRemaining, retryOnResultFalse);
    }
    else
    {
        if (callback != null && callback != undefined)
            callback(error, resultData);
    }
}

module.exports = CallWebservice;