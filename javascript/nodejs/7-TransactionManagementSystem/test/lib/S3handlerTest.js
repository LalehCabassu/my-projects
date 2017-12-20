/**
 * Created by Life on 3/25/15.
 */


s3handler = require("./../../lib/S3handler.js");

//case1();
function case1()
{
    s3handler.CreateBucket("cs7930-test-bucket");
}

//case2();
function case2() {
    s3handler.UploadTextFile("cs7930-test-bucket", "test", "testContent2");
}

//uploadStreamTest();
function uploadStreamTest()
{
    s3handler.UploadFileStream("cs7930-test-bucket", "webservice", "/Users/Life/Downloads/test-2.txt");
}

//downloadTest();
function downloadTest()
{
    s3handler.DownloadFile("cs7930-test-bucket", "webservice", "/Users/Life/Downloads/test-3.txt")
}

//listObjectsTest();
function listObjectsTest()
{
    s3handler.Setup('us-east-1');
    s3handler.ListObjects("cs7930");
}
