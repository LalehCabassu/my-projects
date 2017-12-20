/**
 * Created by Life on 4/18/15.
 */

function SubStringMessage(tranId, startIndex, endIndex)
{
    this.tranId = tranId;
    this.startIndex = startIndex;
    this.endIndex = endIndex;
}

SubStringMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        startIndex: this.startIndex,
        endIndex: this.endIndex
    });
}

SubStringMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='SubStringMessage')
        throw 'json string does not contain a SubStringMessage';

    var newMessage = new SubStringMessage(obj.tranId, obj.startIndex, obj.endIndex);
    return newMessage;
}

module.exports = SubStringMessage;