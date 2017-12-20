/**
 * Created by Life on 2/6/15.
 */

var webservice = require('./../lib/Webservice.js');
var RegisterMessage = require('./RegisterMessage.js');
var ClientMessage = require('./ClientMessage.js');

var querystring = require('querystring');

var nshost, nsport;

module.exports  =
{
    Setup: function(host, port)
    {
        nshost = host;
        nsport = port;
    },

    Register: function(host, port, callback)
    {
        var message = new RegisterMessage(host, port);
        var json = message.toJSON();
        var post_data = querystring.stringify({ registerMessage: json} );
        webservice(nshost, nsport, '/register', post_data, callback, 50, 10);
    },

    Neighbor: function(processId, callback)
    {
        var message = new ClientMessage(processId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json} );
        webservice(nshost, nsport, '/neighbor', post_data, callback, 50, 10);
    },

    Query: function(processId, callback)
    {
        var message = new ClientMessage(processId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json} );
        webservice(nshost, nsport, '/query', post_data, callback, 50, 10);
    },

    Deregister: function(processId, callback)
    {
        var message = new ClientMessage(processId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json} );
        webservice(nshost, nsport, '/deregister', post_data, callback, 50, 10);
    }
}
