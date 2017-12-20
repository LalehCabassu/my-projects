/**
 * Created by Life on 4/18/15.
 */

function WriteMessage(tranId, newValue)
{
    this.tranId = tranId;
    this.newValue = newValue;
}

WriteMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        newValue: this.newValue
    });
}

WriteMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='WriteMessage')
        throw 'json string does not contain a WriteMessage';

    var newMessage = new WriteMessage(obj.tranId, obj.newValue);
    return newMessage;
}

module.exports = WriteMessage;