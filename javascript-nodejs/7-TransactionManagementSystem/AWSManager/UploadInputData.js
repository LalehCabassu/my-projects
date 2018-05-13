/**
 * Created by Life on 3/30/15.
 */

var s3handler = require('./../lib/S3handler.js');

var path = '/Users/Life/Documents/workspaces/nodejs/hw4/Node/InputData';
var bucket = 'cs7930-distributed-mutualexclusion-read';
var inputDirectory = 'input';

s3handler.Setup('us-east-1');
//s3handler.CreateBucket(bucket);
s3handler.DeleteBucket(bucket);
//s3handler.UploadDirectoryInDirectory(bucket, inputDirectory, path);
s3handler.ListObjects(bucket);