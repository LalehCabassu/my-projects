/**
 * Created by Life on 2/6/15.
 */

var webservice = require('./../lib/Webservice.js');
var RegisterMessage = require('./RegisterMessage.js');
var ClientMessage = require('./ClientMessage.js');
var ResourceManagerMessage = require('./ResourceManagerMessage.js');

var querystring = require('querystring');

var nshost, nsport;

module.exports  =
{
    Setup: function(host, port)
    {
        nshost = host;
        nsport = port;
    },

    Register: function(name, host, port, callback)
    {
        var message = new RegisterMessage(name, host, port);
        var json = message.toJSON();
        var post_data = querystring.stringify({ registerMessage: json} );
        webservice(nshost, nsport, '/register', post_data, callback, 50, 10);
    },

    RegisterResourceManager: function(resourceManager, resource, callback)
    {
        var message = new ResourceManagerMessage(resource, resourceManager);
        var json = message.toJSON();
        var post_data = querystring.stringify({ resourceManagerMessage: json} );
        webservice(nshost, nsport, '/registerresourcemanager', post_data, callback, 50, 10);
    },

    Query: function(name, callback)
    {
        var message = new ClientMessage(name);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json} );
        webservice(nshost, nsport, '/query', post_data, callback, 50, 10);
    },

    QueryResourceManager: function(name, callback)
    {
        var message = new ClientMessage(name);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json});
        webservice(nshost, nsport, '/queryresourcemanager', post_data, callback, 50, 10);
    },

    Deregister: function(name, callback)
    {
        var message = new ClientMessage(name);
        var json = message.toJSON();
        var post_data = querystring.stringify({ clientMessage: json} );
        webservice(nshost, nsport, '/deregister', post_data, callback, 50, 10);
    }
}
