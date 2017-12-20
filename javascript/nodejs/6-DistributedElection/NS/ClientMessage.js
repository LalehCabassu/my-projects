/**
 * Created by Life on 3/26/15.
 */

// Constructor
function ClientMessage(processId) {
    this.processId = processId;
}

ClientMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        processId: this.processId
    });
}

ClientMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='ClientMessage')
        throw 'json string does not contain a ClientMessage';

    var newClientMessage = new ClientMessage(obj.processId);
    return newClientMessage;
}

module.exports = ClientMessage;