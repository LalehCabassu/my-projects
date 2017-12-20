/**
 * Created by Life on 4/8/15.
 */

// Constructor
function ElectionMessage(processId) {
    this.processId = processId;
}

ElectionMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        processId: this.processId
    });
}

ElectionMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='ElectionMessage')
        throw 'json string does not contain an ElectionMessage';

    var newElectionMessage = new ElectionMessage(obj.processId);
    return newElectionMessage;
}

module.exports = ElectionMessage;