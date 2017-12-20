/**
 * Created by Life on 1/18/15.
 */

var fs = require('fs');
var path = require('path');

module.exports = {
    FilterFiles: function (dir, ext, callback) {
        fs.readdir(dir, function (err, files) {
            if (err)
                callback(err, null);
            else {
                var filteredFiles = files.filter(function (file) {
                    return path.extname(file) == ext;
                });
                callback(null, filteredFiles);
            }
        });
    },

    ListFiles: function (dir, callback) {
        fs.readdir(dir, function (err, files) {
            if (err)
                 callback(err, null);
            else {
                callback(null, files);
            }
        });
    }
}