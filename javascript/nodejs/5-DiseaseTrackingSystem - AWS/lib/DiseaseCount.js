VectorTimestamp = require('./VectorTimestamp.js');

// Constructor
function DiseaseCount(district, disease, delta, ts)
{
    this.district = district;
    this.disease = disease;
    this.delta = delta;
    this.timestamp = ts;
}

DiseaseCount.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        district: this.district,
        disease: this.disease,
        delta: this.delta,
        timestamp: this.timestamp.toJSON()
    });
}

DiseaseCount.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='DiseaseCount')
        throw 'json string does not contain an alert';

    var ts = VectorTimestamp.fromJSON(obj.timestamp);
    var newDiseaseCount = new DiseaseCount(obj.district, obj.disease, obj.delta, ts);
    newDiseaseCount.reportedOn = new Date(obj.reportedOn);
    return newDiseaseCount;
}

module.exports = DiseaseCount;