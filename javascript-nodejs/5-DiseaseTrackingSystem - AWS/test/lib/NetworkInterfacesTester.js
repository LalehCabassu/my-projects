var assert = require('assert');

var NetworkInterfaces = require("./../../lib/NetworkInterfaces.js");

var addresses = NetworkInterfaces.GetAllAddresses();
console.log('All IPv4 Addresses:');
console.log(addresses);

assert.equal(addresses.length > 0, true);

var bestAddress = NetworkInterfaces.GetBestPublicAddress();
console.log('Best Public Address:');
console.log(bestAddress);
