/**
 * Created by Life on 4/21/15.
 */

var webservice = require('./../lib/Webservice.js');
var TransactionMessage = require('./TransactionMessage.js');
var RegisterMessage = require('./../NS/RegisterMessage.js');

var querystring = require('querystring');

var tmhost, tmport;

module.exports  =
{
    Setup: function(host, port)
    {
        tmhost = host;
        tmport = port;
    },

    Register: function(name, host, port, callback)
    {
        var message = new RegisterMessage(name, host, port);
        var json = message.toJSON();
        var post_data = querystring.stringify({ registerMessage: json} );
        webservice(tmhost, tmport, '/register', post_data, callback, 50, 10);
    },

    Start: function(callback)
    {
        var post_data = querystring.stringify({ generalMessage: ""} );
        webservice(tmhost, tmport, '/start', post_data, callback, 50, 10);
    },

    Commit: function(tranId, resourceManagers, callback)
    {
        var message = new TransactionMessage(tranId, resourceManagers);
        var json = message.toJSON();
        var post_data = querystring.stringify({ transactionMessage: json} );
        webservice(tmhost, tmport, '/commit', post_data, callback, 50, 10);
    },

    Abort: function(tranId, resourceManagers, callback)
    {
        var message = new TransactionMessage(tranId, resourceManagers);
        var json = message.toJSON();
        var post_data = querystring.stringify({ transactionMessage: json} );
        webservice(tmhost, tmport, '/abort', post_data, callback, 50, 10);
    }
}
