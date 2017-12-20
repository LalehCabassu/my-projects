/**
 * Created by Life on 2/4/15.
 */

function ResourceManagerMessage(resource, resourceManager, host, port)
{
    this.resource = resource;
    this.resourceManager = resourceManager;
    this.host = host;
    this.port = port;
}

ResourceManagerMessage.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        resource: this.resource,
        resourceManager: this.resourceManager,
        host: this.host,
        port: this.port
    });
}

ResourceManagerMessage.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type!='ResourceManagerMessage')
        throw 'json string does not contain a ResourceManagerMessage';

    var newMessage = new ResourceManagerMessage(obj.resource, obj.resourceManager, obj.host, obj.port);
    return newMessage;
}

module.exports = ResourceManagerMessage;