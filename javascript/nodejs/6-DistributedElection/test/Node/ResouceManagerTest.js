/**
 * Created by Life on 3/30/15.
 */

var ResourceManager = require('./../../Node/ResourceManager.js');
var DataRecord = require('./../../Node/DataRecord.js');

setupTest();
function setupTest()
{
    ResourceManager.Active(function(err, data){
        if(!err)
            ResourceManager.GetStringPair(function(err, data){
                if(!err)
                {
                    var dataRecord = new DataRecord(data.recordId, data.string1, data.string2, 20);
                    console.log("Pair: " + dataRecord.toString());
                    ResourceManager.SaveResult(dataRecord, function(err, data)
                    {
                        if(!err)
                        {
                            console.log("Saved output file (" + data + ")");
                            ResourceManager.Deactive();
                        }
                    });
                }
            });
    });
}