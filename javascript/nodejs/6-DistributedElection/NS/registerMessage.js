/**
 * Created by Life on 3/26/15.
 */

// Constructor
function RegisterMessage(host, port)
{
    this.host = host;
    this.port = port;
}

RegisterMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        host: this.host,
        port: this.port
    });
}

RegisterMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='RegisterMessage')
        throw 'json string does not contain a RegisterMessage';

    var newRegisterMessage = new RegisterMessage(obj.host, obj.port);
    return newRegisterMessage;
}

module.exports = RegisterMessage;
