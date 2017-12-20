/**
 * Created by Life on 4/18/15.
 */

function ConcatenateMessage(tranId, value1, value2)
{
    this.tranId = tranId;
    this.value1 = value1;
    this.value2 = value2;
}

ConcatenateMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        value1: this.value1,
        value2: this.value2
    });
}

ConcatenateMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='ConcatenateMessage')
        throw 'json string does not contain a ConcatenateMessage';

    var newMessage = new ConcatenateMessage(obj.tranId, obj.value1, obj.value2);
    return newMessage;
}

module.exports = ConcatenateMessage;