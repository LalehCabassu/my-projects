console.log(process.argv);
var fs = require('fs');
var dir = require("./DirectoryReader.js");

function outputFunction(err, data)
{
    for (var i=0; i<data.length; i++)
    {
        var fullFileName = process.argv[2]+'/'+data[i];
        var stat = fs.statSync(fullFileName);
        console.log(fullFileName);
        console.log(stat);
    }
}

if (process.argv.length >= 4)
{
    var path = process.argv[2];
    var ext = process.argv[3];
    console.log('Files with ' + ext + ' in ' + path);

    if (ext == '')
        dir.fullDirectory(path, outputFunction)
    else
        dir.searchDirectory(path, ext, outputFunction);

    console.log("Doing stuff...");
}
else if (process.argv.length >= 3)
{
    console.log('No extension specified');
}
else
{
    console.log('No path specified');
}
