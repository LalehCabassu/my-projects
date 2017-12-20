VectorTimestamp = require('./VectorTimestamp.js');

// Constructor
function Notification(emr, disease, ts)
{
    this.emr = emr;
    this.disease = disease;
    this.reportedOn = new Date();
    this.timestamp = ts;
}

Notification.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        emr: this.emr,
        disease: this.disease,
        reportedOn: this.reportedOn,
        timestamp: this.timestamp.toJSON()
    });
}

Notification.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='Notification')
        throw 'json string does not contain a notification';

    var ts = VectorTimestamp.fromJSON(obj.timestamp);
    var newNotification = new Notification(obj.emr, obj.disease, ts);
    newNotification.reportedOn = new Date(obj.reportedOn);
    return newNotification;
}

module.exports = Notification;
