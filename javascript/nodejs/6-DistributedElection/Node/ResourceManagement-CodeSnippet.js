// Code snippets from Stephen's Resource Manager

function LoadCurrentInputFile(callback)
{
    var params = {Bucket: remoteBucket, Key: stateData.currentInputFile};
    console.log('Loading ' + stateData.currentInputFile);
    s3.getObject(params, function (error, data)
    {
        if (error!=null) callback('Cannot retrieve input file ' + stateData.currentInputFile, null);
        else
        {
            inputBuffer = data.Body;
            console.log('Got it!');
            callback(null, true);
        }
    });
}

function GetNextFile(callback)
{
    var params = {Bucket: remoteBucket, Marker: stateData.currentInputFile, MaxKeys: 1};
    s3.listObjects(params, function (error, data)
    {
        if (error != null)
            callback(error, error.stack);
        else if (data == null || data == undefined || data.Contents == undefined)
            callback('Error: unexpected object list returned from S3');
        else if (data.Contents.length==0 || !data.Contents[0].Key.startsWith(remoteInputFolder))
        {
            inputExhausted = true;
            callback('EOF', null);
        }
        else
        {
            stateData.currentInputFile = data.Contents[0].Key;
            stateData.currentInputPos = 0;
            LoadCurrentInputFile(callback);
        }
    });
}

function LocalGetStringPair(callback)
{
    if (inputExhausted)
        callback('EOF', null);
    else if (stateData.currentInputPos >= inputBuffer.length)
        GetNextFile(function(error, data)
            {
                if (error!=null) callback(error, error.stack);
                else LocalGetStringPair(callback);
            });
    else
    {
        var nextLineEnd = FindNextLineEnd();
        var lineBuffer = inputBuffer.slice(stateData.currentInputPos, nextLineEnd);
        var record = lineBuffer.toString();
        var result = record.split(',');
        stateData.currentInputPos = nextLineEnd + 2;
        recordsReadSinceLastStateSave++;

        if (recordsReadSinceLastStateSave >= saveFrequency)
        {
            SaveStateToS3(function (error, data)
                {
                    recordsReadSinceLastStateSave = 0;
                    if (error != null) callback(error, error.stack);
                    else callback(null, result);
                });
        }
        else
            callback(null, result);
    }
}

function FindNextLineEnd()
{
	//
}
