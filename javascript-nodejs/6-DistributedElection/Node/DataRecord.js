/**
 * Created by Life on 4/2/15.
 */

function DataRecord(recordId, string1, string2, distance)
{
    this.recordId = recordId;
    this.string1 = string1;
    this.string2 = string2;
    this.distance = distance;
}

DataRecord.prototype.toString = function() {
    var result = '';
    result += this.recordId + ',' + this.string1 + ',' + this.string2 + ',' + this.distance;
    return result;
}

DataRecord.prototype.toJSON = function() {
    return JSON.stringify({
        _type: this.constructor.name,
        recordId: this.recordId,
        string1: this.string1,
        string2: this.string2,
        distance: this.distance
    });
}

DataRecord.fromString = function(string)
{
    var record = string.split(',');
    var newDataRecord;
    if(record[3] == undefined)
        newDataRecord = new DataRecord(record[0], record[1], record[2].substr(0,record[2].length - 1), null);
    else
        newDataRecord = new DataRecord(record[0], record[1], record[2], record[3]);
    return newDataRecord;
}

DataRecord.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='DataRecord')
        throw 'json string does not contain a DataRecord';

    var newDataRecord = new DataRecord(obj.recordId,
        obj.string1, obj.string2, obj.distance);
    return newDataRecord;
}

module.exports = DataRecord;