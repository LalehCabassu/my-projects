var assert = require('assert');
var Client = require('./../../NS/NameServerClient.js');

var host = process.argv[2];
var port = process.env.PORT || parseInt(process.argv[3]) || 2499;

Client.Setup(host, port);                   // Setup the host and port number for the client
case0();

// Try registering first process
function case0() {
    Client.Register('RM01', '129.123.7.22', 3332, function (error, data) {
        console.log('data: ' + data);
        assert.equal(data, true);
        console.log('case0\tSuccessful');
        case1();
    });
}

function case1() {
    Client.RegisterResourceManager('RM01', 'R01', function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        console.log('case1\tSuccessful');
        case2();
    });
}

function case2() {
    Client.Query('RM01', function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data.name, 'RM01');
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case2\tSuccessful');
        case3();
    });
}

function case3() {
    Client.QueryResourceManager('R01', function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data.resource, 'R01');
        assert.equal(data.resourceManager, 'RM01');
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case3\tSuccessful');
    });
}
