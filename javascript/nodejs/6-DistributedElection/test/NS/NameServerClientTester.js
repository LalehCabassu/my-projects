var assert = require('assert');
var Client = require('./../../NS/NameServerClient.js');

var host = process.argv[2];
var port = process.env.PORT || parseInt(process.argv[3]) || 2499;

Client.Setup(host, port);                   // Setup the host and port number for the client
case0();

// Try registering first process
function case0() {
    Client.Register('129.123.7.22', 3332, function (error, data) {
        console.log('data: ' + data);
        assert.equal(data.processId, 0);
        console.log('case0\tSuccessful');
        case1();
    });
}

// Try registering second process
function case1() {
    Client.Register('129.123.7.23', 3332, function (error, data) {
        console.log('data: ' + data);
        assert.equal(data.processId, 1);
        console.log('case1\tSuccessful');
        case2();
    });
}

// Try getting the neighbor of process0
function case2() {
    Client.Neighbor(0, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data.host, '129.123.7.23');
        assert.equal(data.port, 3332);
        console.log('case2\tSuccessful');
        case3();
    });
}

// Try getting the neighbor of process1
function case3() {
    Client.Neighbor(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case3\tSuccessful');
        case4();
    });
}

function case4() {
    Client.Query(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data.host, '129.123.7.23');
        assert.equal(data.port, 3332);
        console.log('case4\tSuccessful');
        case5();
    });
}

function case5() {
    Client.Deregister(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        console.log('case4\tSuccessful');
    });
}