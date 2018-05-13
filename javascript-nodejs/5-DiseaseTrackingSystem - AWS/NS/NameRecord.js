/**
 * Created by Life on 2/4/15.
 */

function NameRecord(type, host, port)
{
    this.type = type;
    this.host = host;
    this.port = port;
}

NameRecord.prototype.toString = function()
{
    return String('Type: ' + this.type + ', Host: ' + this.host + ', Port: ' + this.port);
}

NameRecord.prototype.toJson = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        type: this.type,
        host: this.host,
        port: this.port
    });
}

NameRecord.fromJson = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type != 'NameRecord')
        throw 'json string does not contain a name record';

    var newRecord = new NameRecord(obj.type, obj.host, obj.port);
    return newRecord;
}

module.exports = NameRecord;