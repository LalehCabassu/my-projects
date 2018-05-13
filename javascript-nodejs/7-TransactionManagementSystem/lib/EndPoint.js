/**
 * Created by Life on 2/4/15.
 */

function EndPoint(host, port)
{
    this.host = host;
    this.port = port;
}

EndPoint.prototype.toString = function()
{
    return String('Host: ' + this.host + ', Port: ' + this.port);
}

module.exports = EndPoint;