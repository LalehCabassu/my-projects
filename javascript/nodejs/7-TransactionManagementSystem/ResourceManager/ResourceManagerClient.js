/**
 * Created by Life on 4/28/15.
 */

var webservice = require('./../lib/Webservice.js');
var LockMessage = require('./LockMessage.js');
var GeneralMessage = require('./../lib/GeneralMessage.js');
var WriteMessage = require('./WriteMessage.js');
var ConcatenateMessage = require('./ConcatenateMessage.js');
var TruncateMessage = require('./TruncateMessage.js');
var SubStringMessage = require('./SubStringMessage.js');

var querystring = require('querystring');

var rmhost, rmport;

module.exports  =
{
    Setup: function(host, port)
    {
        rmhost = host;
        rmport = port;
    },

    Lock: function(tranId, type, timeout, callback)
    {
        var message = new LockMessage(tranId, type, timeout);
        var json = message.toJSON();
        var post_data = querystring.stringify({ lockMessage: json} );
        webservice(rmhost, rmport, '/lock', post_data, callback, 50, 10);
    },

    Read: function(tranId, callback)
    {
        var message = new GeneralMessage(tranId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ generalMessage: json} );
        webservice(rmhost, rmport, '/read', post_data, callback, 50, 10);
    },

    Write: function(tranId, newValue, callback)
    {
        var message = new WriteMessage(tranId, newValue);
        var json = message.toJSON();
        var post_data = querystring.stringify({ writeMessage: json} );
        webservice(rmhost, rmport, '/write', post_data, callback, 50, 10);
    },

    Concatenate: function(tranId, value1, value2, callback)
    {
        var message = new ConcatenateMessage(tranId, value1, value2);
        var json = message.toJSON();
        var post_data = querystring.stringify({ concatenateMessage: json} );
        webservice(rmhost, rmport, '/concatenate', post_data, callback, 50, 10);
    },

    Truncate: function(tranId, index, callback)
    {
        var message = new TruncateMessage(tranId, index);
        var json = message.toJSON();
        var post_data = querystring.stringify({ truncateMessage: json} );
        webservice(rmhost, rmport, '/truncate', post_data, callback, 50, 10);
    },

    SubString: function(tranId, startIndex, endIndex, callback)
    {
        var message = new SubStringMessage(tranId, startIndex, endIndex);
        var json = message.toJSON();
        var post_data = querystring.stringify({ subStringMessage: json} );
        webservice(rmhost, rmport, '/substring', post_data, callback, 50, 10);
    },

    ToUpper: function(tranId, callback)
    {
        var message = new GeneralMessage(tranId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ generalMessage: json} );
        webservice(rmhost, rmport, '/toupper', post_data, callback, 50, 10);
    },

    ToLower: function(tranId, callback)
    {
        var message = new GeneralMessage(tranId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ generalMessage: json} );
        webservice(rmhost, rmport, '/tolower', post_data, callback, 50, 10);
    },

    Unlock: function(tranId, callback)
    {
        var message = new GeneralMessage(tranId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ generalMessage: json} );
        webservice(rmhost, rmport, '/unlock', post_data, callback, 50, 10);
    },

    Rollback: function(tranId, callback)
    {
        var message = new GeneralMessage(tranId);
        var json = message.toJSON();
        var post_data = querystring.stringify({ generalMessage: json} );
        webservice(rmhost, rmport, '/rollback', post_data, callback, 50, 10);
    }
}
