var diseases = require('../lib/Diseases.js');

var diseaseCounts = new Array(diseases.COUNT);
for (var i=0; i<diseases.COUNT; i++)
    diseaseCounts[i] = 0;

module.exports = {

    GetCounts: function()
    {
        return diseaseCounts;
    },

    IncrementCount: function(diseaseIndex)
    {
        if (diseaseIndex>=0 && diseaseIndex< diseases.COUNT)
            diseaseCounts[diseaseIndex] = diseaseCounts[diseaseIndex] + 1;
    }
};

