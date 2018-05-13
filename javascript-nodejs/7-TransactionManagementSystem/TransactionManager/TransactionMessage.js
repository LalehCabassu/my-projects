/**
 * Created by Life on 4/21/15.
 */

function CommitMessage(tranId, resourceManagers)
{
    this.tranId = tranId;
    this.resourceManagers = resourceManagers;
}

CommitMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        resourceManagers: this.resourceManagers
    });
}

CommitMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='CommitMessage')
        throw 'json string does not contain a CommitMessage';

    var newMessage = new CommitMessage(obj.tranId, obj.resourceManagers);
    return newMessage;
}

module.exports = CommitMessage;