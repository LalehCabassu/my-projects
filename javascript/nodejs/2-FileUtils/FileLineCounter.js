var fs = require('fs');

console.log(process.argv);
if (process.argv.length >= 3)
{
    console.log('Count the lines in ' + process.argv[2]);

    fs.readFile(process.argv[2], {encoding: 'utf8'}, function(err, data) {
        if (err) throw err;
        var lines = data.toString().split(String.fromCharCode(13));
        console.log('Number of lines = ' + String(lines.length));
        });
    console.log("Doing stuff...");
}
else
{
    console.log('No file specified');
}
console.log('end of code');