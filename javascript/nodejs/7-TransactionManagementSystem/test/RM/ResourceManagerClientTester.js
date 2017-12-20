/**
 * Created by Life on 4/28/15.
 */

var assert = require('assert');
var Client = require('./../../ResourceManager/ResourceManagerClient.js');

var host = process.argv[2];
var port = parseInt(process.argv[3]);

Client.Setup(host, port);                   // Setup the host and port number for the client
case0();

// Try registering first process
function case0() {
    Client.Lock(1, 'read', 500, function (error, data) {
        console.log('data: ' + data);
        assert.equal(data, true);
        console.log('case0\tSuccessful');
        case1();
    });
}

function case1() {
    Client.Read(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, 'Laleh');
        console.log('case1\tSuccessful');
        case2();
    });
}

function case2() {
    Client.Lock(1, 'write', 500, function (error, data) {
        console.log('data: ' + data);
        assert.equal(data, true);
        console.log('case2\tSuccessful');
        case3();
    });
}

function case3() {
    Client.Write(1, 'Tulip', function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        Client.Read(1, function (error, data) {
            console.log('data: ' + data);
            assert.equal(error == null, true);
            assert.equal(data, 'Tulip');
            console.log('case3\tSuccessful');
            case4();
        });
    });
}

function case4() {
    Client.Concatenate(1, 'Laleh: ', 'Tulip', function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        Client.Read(1, function (error, data) {
            console.log('data: ' + data);
            assert.equal(error == null, true);
            assert.equal(data, 'Laleh: Tulip');
            console.log('case4\tSuccessful');
            //case5();
            case7();
        });
    });
}

function case5() {
    Client.Unlock(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        console.log('case5\tSuccessful');
        case6();
    });
}

function case6() {
    Client.Read(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, null);
        console.log('case6\tSuccessful');
    });
}

function case7() {
    Client.Rollback(1, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        Client.Lock(1, 'read', 500, function (error, data) {
            console.log('data: ' + data);
            Client.Read(1, function (error, data) {
                console.log('data: ' + data);
                assert.equal(error == null, true);
                assert.equal(data, 'Laleh');
                console.log('case5\tSuccessful');
            });
        });
    });
}
