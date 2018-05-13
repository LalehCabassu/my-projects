/**
 * Created by Life on 2/4/15.
 */

var ALIVENESS = 50;

function NameRecord(host, port)
{
    this.host = host;
    this.port = port;
    this.aliveness = ALIVENESS;
}

NameRecord.prototype.toString = function()
{
    return String('Host: ' + this.host + ', Port: ' + this.port, ', Aliveness: ', this.aliveness);
}
NameRecord.prototype.isAlive = function() {
    return (this.aliveness <= 0) ? false : true;
}

module.exports = NameRecord;