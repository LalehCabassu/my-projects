/**
 * Created by Life on 4/17/15.
 */

function LockMessage(tranId, type, timeout)
{
    this.tranId = tranId;
    this.type = type;
    this.timeout = timeout;
}

LockMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        tranId: this.tranId,
        type: this.type,
        timeout: this.timeout
    });
}

LockMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='LockMessage')
        throw 'json string does not contain a LockMessage';

    var newLockMessage = new LockMessage(obj.tranId, obj.type, obj.timeout);
    return newLockMessage;
}

module.exports = LockMessage;