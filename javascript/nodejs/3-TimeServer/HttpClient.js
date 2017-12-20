var http = require('http');
var BufferList = require('bl');
var dataList = [];
var urls = [];

console.log(process.argv);

function AllComplete()
{
    var resultCount = 0;
    for (var i=0; i<urls.length; i++)
    {
        if (dataList[i]!=null)
            resultCount++;
    }
    return resultCount==urls.length;
}

function ProcessRequest(requestIndex, response)
{
    var data = new BufferList();
    response.setEncoding('utf8');

    response.on("data", function(chunk) {
        console.log("receiving a chunk")
        data.append(chunk);
    });

    response.on("end", function() {
        dataList[requestIndex] = data.toString();
        if (AllComplete())
        {
            for (var i=0; i<dataList.length; i++)
            {
                console.log("Url=" + urls[i] );
                console.log("Response >>>>>>");
                console.log(dataList[i]);
            }
        }
    });
}

if (process.argv.length>=3)
{
    urls = process.argv.slice(2);
    for (i=0; i<urls.length; i++)
        dataList[i] = null;

    for (i=0; i<urls.length; i++)
    {
        (function(i) {
            console.log("Retrieve " + urls[i]);
            http.get(urls[i], function (response) {
                ProcessRequest(i, response);
            }).on("error", function (e) {
                console.log("Got error: " + e.message);
            });
        })(i);
    }
}
else
{
    console.log("No URL provided");
}
