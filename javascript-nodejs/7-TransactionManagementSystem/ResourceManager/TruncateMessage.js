/**
 * Created by Life on 4/18/15.
 */

function TruncateMessage(tranId, index)
{
    this.tranId = tranId;
    this.index = index;
}

TruncateMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        index: this.index
    });
}

TruncateMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='TruncateMessage')
        throw 'json string does not contain a TruncateMessage';

    var newMessage = new TruncateMessage(obj.tranId, obj.index);
    return newMessage;
}

module.exports = TruncateMessage;