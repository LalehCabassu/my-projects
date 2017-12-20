var diseases = require('./../lib/Diseases.js');

// Constructor
function EmrData(emrId)
{
    this.emrId = emrId;
    this.totalCounts = new Array(diseases.COUNT);
    this.deltaCounts = new Array(diseases.COUNT);
    for (var i=0; i<diseases.COUNT; i++)
    {
        this.totalCounts[i] = 0;
        this.deltaCounts[i] = 0;
    }
}

EmrData.prototype.TotalCounts = function()
{
    return this.totalCounts;
};

EmrData.prototype.DeltaCounts = function()
{
    return this.deltaCounts;
};

EmrData.prototype.ClearDeltaCounts = function()
{
    for (var i=0; i<diseases.COUNT; i++)
        this.deltaCounts[i] = 0;
};

EmrData.prototype.AddTo = function(diseaseIndex)
{
    if (diseaseIndex >= 0 && diseaseIndex < diseases.COUNT)
    {
        this.totalCounts[diseaseIndex] = this.totalCounts[diseaseIndex] + 1;
        this.deltaCounts[diseaseIndex] = this.deltaCounts[diseaseIndex] + 1;
    }
};

EmrData.prototype.GetDelta = function()
{
    var result = new Array(diseases.COUNT);
    for (var i=0; i<diseases.COUNT; i++)
    {
        result[i] = this.deltaCounts[i];
        this.deltaCounts[i] = 0;
    }
    return result;
};

module.exports = EmrData;
