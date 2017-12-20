var fs = require('fs');

function internalFoo()
{

}

module.exports.fullDirectory = function(path, callback)
{

}

module.exports.searchDirectory = function(path, ext, callback)
{
    fs.readdir(path, function(err, data) {
        if (err)
            callback(err, null)
        else {
            var filteredData = data.filter(function (filename) {
                var pos = filename.lastIndexOf(ext)
                return pos == filename.length - ext.length;
            });
            callback(null, filteredData);
        }
    });
};

