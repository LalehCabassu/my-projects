/**
 * Created by Life on 2/9/15.
 */

var instances = [];

module.exports = {
    AddInstance: function(instanceId, publicDns) {
        instances.push({instanceId: instanceId, publicDns: publicDns});
    },

    GetInstance: function(index)
    {
        return instances[index];
    },

    GetInstanceIds: function()
    {
        var instanceIds = [];
        for(var i = 0;  i < instances.length; i++)
            instanceIds.push(instances[i].instanceId);
        return instanceIds;
    },

    GetInstancePublicDns: function()
    {
        var instanceIds = [];
        for(var i = 0;  i < instances.length; i++)
            instanceIds.push(instances[i].publicDns);
        return instanceIds;
    },

    GetLength: function()
    {
        return instances.length;
    }
}