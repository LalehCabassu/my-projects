var VectorTimestamp = require('../lib/VectorTimestamp.js');
var Notification = require('../lib/Notification.js');
var diseases = require('../lib/Diseases.js');
var EmrRecord = require('./EmrData.js');

var emrs = [];
var diseaseTotals = new Array(diseases.COUNT);
for (var i=0; i<diseases.COUNT; i++)
    diseaseTotals[i] = 0;

function GetEmrIndex(emrId)
{
    var result = -1;
    for (var i=0; i<emrs.length && result<0; i++)
    {
        if (emrs[i].emrId == emrId)
            result = i;
    }
    return result;
}

function AddEmr(emrId)
{
    var emrRecord =  new EmrRecord(emrId);
    var newLength = emrs.push(emrRecord);
    return newLength - 1;
}

module.exports =
{
    TotalCounts: function()
    {
        return diseaseTotals;
    },

    Record: function(notification)
    {

        if (notification != null && notification.constructor.name == 'Notification')
        {
            var emrIndex = GetEmrIndex(notification.emr);
            if (emrIndex<0)
                emrIndex = AddEmr(notification.emr);

            emrs[emrIndex].AddTo(notification.disease);
        }
    },

    GetDeltaCounts: function()
    {
        var result = new Array(diseases.COUNT);
        for (var i=0; i<diseases.COUNT; i++)
            result[i] = 0;
        for (var i=0; i<emrs.length; i++)
        {
            var deltas = emrs[i].GetDelta();
            for (var j=0; j<diseases.COUNT; j++)
               result[j] += deltas[j];
        }
        return result;
    }

}