/**
 * Created by Life on 1/18/15.
 */

var fs = require('fs');
var path = require('path');

module.exports.listFiles = function(dir, extension, callback, res)
{
    fs.readdir(dir, function(err, files)
    {
        if(err)
            callback(err, null, null);
        else {
            var filteredFiles = files.filter(function (file) {
                return path.extname(file) == extension;
            });
            callback(null, filteredFiles, res);
        }
    });
}