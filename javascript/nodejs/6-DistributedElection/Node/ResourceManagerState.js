/**
 * Created by Life on 3/30/15.
 */

function ResourceManagerState(resourceManagerId, inputFileIndex, outputFileIndex, readRecord, writeRecord, isElectionInProcess)
{
    this.resourceManagerId = resourceManagerId;
    this.inputFileIndex = inputFileIndex;
    this.outputFileIndex = outputFileIndex;
    this.readRecord = readRecord;
    this.writeRecord = writeRecord
    this.electionInProcess = isElectionInProcess;
}

ResourceManagerState.prototype.toJSON = function() {

    return JSON.stringify({
        _type: this.constructor.name,
        resourceManagerId: this.resourceManagerId,
        inputFileIndex: this.inputFileIndex,
        outputFileIndex: this.outputFileIndex,
        readRecord: this.readRecord,
        writeRecord: this.writeRecord,
        electionInProcess: this.electionInProcess
    });
}

ResourceManagerState.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='ResourceManagerState')
        throw 'json string does not contain a ResourceManagerState';

    var newResourceManagerState = new ResourceManagerState(obj.resourceManagerId,
                                        obj.inputFileIndex, obj.outputFileIndex,
                                        obj.readRecord, obj.writeRecord, obj.electionInProcess);
    return newResourceManagerState;
}

module.exports = ResourceManagerState;