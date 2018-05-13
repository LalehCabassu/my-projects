/**
 * Created by Life on 3/25/15.
 */

var AWS = require('aws-sdk');
var fs = require('fs');

var directoryList = require('./DirectoryList.js');

AWS.config.region = 'us-west-2';
var s3 = new AWS.S3();

var functions =
{
    Setup: function(region)
    {
        AWS.config.region = region;
        s3 = new AWS.S3();
    },

    ListObjectsInDirectory: function(bucketName, marker, callback)
    {
        var params = { Bucket: bucketName, Marker: marker };
        s3.listObjects(params, function(err, data) {
            if(err) {
                console.log("Error in listing objects in bucket(" + bucketName + "). " +
                "\nMessage: ", err.stack);
                callback(err, null);
            }
            else {
                var files = [];
                for(var index in data.Contents) {
                    var object = data.Contents[index];
                    files[index] = object.Key;
                }
                callback(null, files);
            }
        });
    },

    ListObjects: function(bucketName, callback)
    {
        var params = { Bucket: bucketName };
        s3.listObjects(params, function(err, data) {
           if(err) {
               console.log("Error in listing objects in bucket(" + bucketName + "). " +
               "\nMessage: ", err.stack);
               callback(err, null);
           }
           else {
               var files = [];
               for(var index in data.Contents) {
                   var object = data.Contents[index];
                   files[index] = object.Key;
               }
               callback(null, files);
           }
        });
    },

    FilterObjects: function(bucketName, startWith, callback)
    {
        var params = { Bucket: bucketName,  Prefix: startWith};
        s3.listObjects(params, function(err, data) {
            if(err) {
                console.log("Error in listing objects in bucket(" + bucketName + "). " +
                "\nMessage: ", err.stack);
                callback(err, null);
            }
            else {
                var files = [];
                for(var index in data.Contents) {
                    var object = data.Contents[index];
                    files[index] = object.Key;
                }
                callback(null, files);
            }
        });
    },

    CreateBucket: function(bucketName)
    {
        var params = {Bucket: bucketName};

        s3.createBucket(params, function(err) {
            if (err) { console.log("Error in creating bucket(" + bucketName + "). " +
                                    "\nMessage: ", err); }
            else {
                console.log("Successfully created bucket (" + bucketName + ")");
            }
        });
    },

    UploadTextFile: function(bucketName, fileName, fileContent, callback)
    {
        var params = {Bucket: bucketName, Key: fileName, Body: fileContent};
        s3.upload(params, function(err, data) {
            callback(err, data);
        });
    },

    UploadFileStream: function(bucketName, fileName, localPath)
    {
        var file = fs.createReadStream(localPath);
        var params = {Bucket: bucketName, Key: fileName, Body: file};
        s3.upload(params, function(err, data) {
            if (err) { console.log("Error in uploading file(" + fileName  + ")" +
                        "in bucket( " + bucketName + "). \nMessage: ", err); }
            else {
                console.log("Successfully uploaded file(" + fileName + ") bucket (" + bucketName + ")");
            }
        });
    },

    UploadFileStreamInDirectory: function(bucketName, directoryName, fileName, localPath)
    {
        var file = fs.createReadStream(localPath);
        var fullPath = directoryName + '/' + fileName
        var params = {Bucket: bucketName, Key: fullPath, Body: file};
        s3.upload(params, function(err, data) {
            if (err) { console.log("Error in uploading file(" + fileName  + ")" +
            "in bucket( " + bucketName + "). \nMessage: ", err); }
            else {
                console.log("Successfully uploaded file(" + fileName + ") bucket (" + bucketName + ")");
            }
        });
    },

    UploadDirectory: function (bucketName, localPath)
    {
        var files = directoryList.ListFiles(localPath, function(err, files){
            if(err != null)
                console.log("Error in reading files from local path.");
            else
                for(var index in files)
                {
                    var fullPath = localPath + '/' + files[index];
                    functions.UploadFileStream(bucketName, files[index], fullPath);
                }
        });
    },

    UploadDirectoryInDirectory: function (bucketName, directoryName, localPath)
    {
        var files = directoryList.ListFiles(localPath, function(err, files){
            if(err != null)
                console.log("Error in reading files from local path.");
            else
                for(var index in files)
                {
                    var fullPath = localPath + '/' + files[index];
                    functions.UploadFileStream(bucketName, directoryName, files[index], fullPath);
                }
        });
    },

    UploadFilteredDirectory: function (bucketName, localPath, ext)
    {
        var files = directoryList.FilterFiles(localPath, ext, function(err, files) {
            if(err != null)
                console.log("Error in reading files from local path.");
            else
                for(var index in files)
                {
                    var fullPath = localPath + '/' + files[index];
                    functions.UploadFileStream(bucketName, files[index], fullPath);
                }
        });
    },

    DownloadFile: function(bucketName, fileName, localPath)
    {
        var params = {Bucket: bucketName, Key: fileName};
        var file = fs.createWriteStream(localPath);
        var readable = s3.getObject(params).createReadStream();
        readable.pipe(file);
    },

    GetFileString: function(bucketName, fileName, callback)
    {
        var params = {Bucket: bucketName, Key: fileName};
        s3.getObject(params, function(err, data) {
            if (err) {
                console.log('Error in getting file (' + fileName + ') bucket (' + bucketName + ')');
                callback(err, null);
            }
            else {
                callback(null, data.Body);
            }
        });
    },

    DeleteBucket: function(bucketName, callback)
    {
        var files = [];
        functions.ListObjects(bucketName, function(err, data) {
           if(!err)
                files = data;
        });

        var objects = [];
        for(var index in files)
        {
            var object = { Key: files[index] };
            objects[index] = object;
        }
        var params = { Bucket: bucketName, Objects: objects };
        s3.deleteObjects(params, function(err, data){
           if(err)
                console.log("Error in deleting files in bucket (" + bucketName + ")");
            else
           {
                s3.deleteBucket({Bucket: bucketName}, function(err, data){
                    if(err)
                        console.log("Error in deleting bucket(" + bucketName + ")");
                    else
                        callback();
                });
                console.log("Successfully deleted bucket (" + bucketName + ")");
           }
        });
    },

    DeleteDirectory: function(bucketName, directoryName)
    {
        var files = [];
        functions.ListObjectsInDirectory(bucketName, directoryName, function(err, data) {
            if(!err)
                files = data;
        });

        var objects = [];
        for(var index in files)
        {
            var object = { Key: files[index] };
            objects[index] = object;
        }
        var params = { Bucket: bucketName, Objects: objects };
        s3.deleteObjects(params, function(err, data){
            if(err)
                console.log("Error in deleting directory (" + directoryName + "), bucket (" + bucketName + ")");
            else
                console.log("Successfully deleted directory (" + directoryName + "), bucket (" + bucketName + ")");
        });
    }
}

module.exports = functions;