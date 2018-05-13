/**
 * Created by Life on 4/17/15.
 */

function GeneralMessage(tranId)
{
    this.tranId = tranId;
}

GeneralMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId
    });
}

GeneralMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='GeneralMessage')
        throw 'json string does not contain a GeneralMessage';

    var newMessage = new GeneralMessage(obj.tranId);
    return newMessage;
}

module.exports = GeneralMessage;