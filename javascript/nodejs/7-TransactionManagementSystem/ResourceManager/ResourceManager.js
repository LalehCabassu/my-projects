/**
 * Created by Life on 3/30/15.
 */

var querystring = require('querystring');
var webservice = require('./../lib/Webservice.js');
var Resource = require('./Resource.js');

var myResource = null;
var readLockHolders = [];
var writeLockHolder = null;
var writeSnapshot = null;
var readLockTryAgain = 5;
var writeLockTryAgain = 5;

module.exports = {
    Active: function (resourceId, resourceValue) {
        myResource = new Resource(resourceId, resourceValue);
        writeSnapshot = myResource.value;
    },

    Lock: function(tranId, type, timeout, callback)
    {
        isConflictingLock(tranId, type, timeout, callback);
    },

    Read: function(tranId, callback)
    {
        hasReadLock(tranId, function(err, data){
                if(data == true)
                    callback(null, myResource.value);
                else
                    callback(null, null);
        });
    },

    Write: function(tranId, newValue, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = newValue;
            console.log("Write-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    Concatenate: function(tranId, value1,  value2, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = value1.concat(value2);
            console.log("Concatenate-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    Truncate: function(tranId, index, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = myResource.value.substring(0, index - 1);
            console.log("Truncate-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    SubString: function(tranId, startIndex, endIndex, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = myResource.value.substring(startIndex, endIndex);
            console.log("SubString-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    ToUpper: function(tranId, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = myResource.value.toUpperCase();
            console.log("ToUpper-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    ToLower: function (tranId, callback)
    {
        if(writeLockHolder == tranId)
        {
            myResource.value = myResource.value.toLowerCase();
            console.log("ToLower-> resource: " +  myResource.value);
            callback(null, true);
        }
        else
            callback(null, false);
    },

    Unlock: function(tranId, callback)
    {
        unlock(tranId, callback);
    },

    Rollback: function(tranId, callback)
    {
        if(writeLockHolder == tranId) {
            myResource.value = writeSnapshot;
            console.log("Rollback-> resource: " +  myResource.value);
        }

        unlock(tranId, callback);
    }
};

// Strict two-phase locking
function isConflictingLock(tranId, type, timeout, callback)
{
    if(type == 'write') {
        // if another transaction is holding the lock
        if (writeLockHolder != null)
        {
            if(writeLockHolder != tranId) {
                if (writeLockTryAgain > 0) {
                    var newTimeOut = timeout - timeout / writeLockTryAgain;
                    console.log("Write lock trying again in " + newTimeOut + "ms. (tranId:" + tranId + ")");
                    setTimeout(isConflictingLock, newTimeOut, tranId, type, newTimeOut, callback);
                    writeLockTryAgain -= 1;
                }
                else
                {
                    writeLockTryAgain = 5;
                    callback(null, false);
                }
            }
            else
            {
                writeLockTryAgain = 5;
                callback(null, true);
            }
        }
        else if(readLockHolders.length > 0)
            hasReadLock(tranId, function(err, data) {
                if(data == true) {
                    writeSnapshot = myResource.value;
                    writeLockHolder = tranId;
                    writeLockTryAgain = 5;
                    callback(null, true);
                }
                else
                {
                    if (writeLockTryAgain > 0) {
                        var newTimeOut = timeout - timeout / writeLockTryAgain;
                        console.log("Write lock trying again in " + newTimeOut + "ms. (tranId:" + tranId + ")");
                        setTimeout(isConflictingLock, newTimeOut, tranId, type, newTimeOut, callback);
                        writeLockTryAgain -= 1;
                    }
                    else
                    {
                        writeLockTryAgain = 5;
                        callback(null, false);
                    }
                }
            });
        else  // no read or write lock is holding or tranId is already holding the read or write lock
        {
            writeSnapshot = myResource.value;
            writeLockHolder = tranId;
            writeLockTryAgain = 5;
            callback(null, true);
        }
    }
    else if(type == 'read') {
        if (writeLockHolder != null)
        {
            if(readLockTryAgain > 0) {
                var newTimeOut = timeout - timeout / readLockTryAgain;
                console.log("Read lock trying again in " + newTimeOut + "ms. (tranId:" + tranId + ")");
                setTimeout(isConflictingLock, newTimeOut, tranId, type, newTimeOut, callback);
                readLockTryAgain -= 1;
            }
            else
            {
                readLockTryAgain = 5;
                callback(null, false);
            }
        }
        else {
            var index = readLockHolders.length;
            readLockHolders[index] = tranId;
            readLockTryAgain = 5;
            callback(null, true);
        }
    }
}

function hasReadLock(tranId, callback)
{
    if(readLockHolders.length == 0)
        callback(null, false);
    else
        for (var i = 0; i < readLockHolders.length; i++)
        {
            if(readLockHolders[i] == tranId) // tranId is already holding a read lock
            {
                callback(null, true);
                break;
            }
            if(i == readLockHolders.length - 1)
                callback(null, false);
        }
}

function unlock(tranId, callback)
{
    if(writeLockHolder == tranId)
        writeLockHolder = null;
    removeReadLockHolder(tranId, callback);
}

function removeReadLockHolder(tranId, callback)
{
    var tempArray = [];
    var j = 0;

    if(readLockHolders.length == 0)
        callback(null, true);
    else
        for(var i = 0; i < readLockHolders.length; i++)
        {
            if(readLockHolders[i] != tranId) {
                tempArray[j] = readLockHolders[i];
                ++j;
            }
            if(i == readLockHolders.length - 1) {
                readLockHolders = tempArray;
                callback(null, true);
            }
        }
}
