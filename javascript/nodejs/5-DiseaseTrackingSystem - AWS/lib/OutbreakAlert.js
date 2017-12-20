VectorTimestamp = require('./VectorTimestamp.js');

// Constructor
function OutbreakAlert(district, disease, ts)
{
    this.district = district;
    this.disease = disease;
    this.alertDate = new Date();
    this.timestamp = ts;
}

OutbreakAlert.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        district: this.district,
        disease: this.disease,
        alertDate: this.alertDate,
        timestamp: this.timestamp.toJSON()
    });
}

OutbreakAlert.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='OutbreakAlert')
        throw 'json string does not contain an outbreak alert';

    var ts = VectorTimestamp.fromJSON(obj.timestamp);
    var newAlert = new OutbreakAlert(obj.district, obj.disease, ts);
    newAlert.reportedOn = new Date(obj.reportedOn);
    return newAlert;
}

module.exports = OutbreakAlert;