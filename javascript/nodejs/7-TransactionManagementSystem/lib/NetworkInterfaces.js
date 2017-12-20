var os = require('os');

function GetIPv4Addresses(includeLocal)
{
    var result = [];
    var interfaces = os.networkInterfaces();
    var interfaceNames = Object.keys(interfaces);
    interfaceNames.forEach(function(interfaceName)
        {
            var addresses = interfaces[interfaceName];
            for (var j = 0; j < addresses.length; j++) {
                if (addresses[j].family === 'IPv4' && (includeLocal === true || addresses[j].internal === false))
                    result[result.length] = addresses[j].address;
            }
        }
    );

    return result;
}

module.exports = {
    GetAllAddresses : function() { return GetIPv4Addresses(true); },
    GetBestPublicAddress : function()
        {
            var addresses = GetIPv4Addresses(false);
            return addresses[0];
        }
}
