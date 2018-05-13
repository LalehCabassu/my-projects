var assert = require('assert');
var Client = require('../../NS/NameServerClient.js');

var host = process.argv[2];
var port = process.env.PORT || parseInt(process.argv[3]) || 2499;

Client.Setup(host, port);                   // Setup the host and port number for the client
case1();

// Make sure doa1 is not in the name registry
function case0() {
    Client.Deregister('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, true);
        console.log('case0\tSuccessful');
        case1();
    });
}

// Make sure hds2 is not in the name registry
function case1() {
    Client.Deregister('hds2', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, true);
        console.log('case1\tSuccessful');
        case2();
    });
}

// Try adding looking up doa1 - no entry (data) should exist.
function case2() {
    Client.Lookup('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case2\tSuccessful');
        case3();
    });
}

// Try registering doa1
function case3() {
    Client.Register('doa1', 'doa', '129.123.7.22', 3332, function (error, data) {
        assert.equal(data, true);
        console.log('case3\tSuccessful');
        case4();
    });
}

// Try looking up doa1 - now an entry (data) exist
function case4() {
    Client.Lookup('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data.name, 'doa1');
        assert.equal(data.type, 'doa');
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case4\tSuccessful');
        case5();
    });
}

// Try deregistering doa1 again
function case5() {
    Client.Deregister('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, true);
        console.log('case5\tSuccessful');
        case6();
    });
}

// Try registering doa1 again
function case6() {
    Client.Register('doa1', 'doa', '129.123.7.22', 3332, function (error, data) {
        assert.equal(data, true);
        console.log('case6\tSuccessful');
        case7();
    });
}

// Try looking up doa1 - should exist
function case7() {
    Client.Lookup('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data.name, 'doa1');
        assert.equal(data.type, 'doa');
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case7\tSuccessful');
        case8();
    });
}

// Try registering another name, hds2
function case8() {
    Client.Register('hds2', 'hds', '54.23.5.23', 2352, function (error, data) {
        assert.equal(data, true);
        console.log('case8\tSuccessful');
        case9();
    });
}

// Try looking up hds2 -- should exist
function case9() {
    Client.Lookup('hds2', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data.name, 'hds2');
        assert.equal(data.type, 'hds');
        assert.equal(data.host, '54.23.5.23');
        assert.equal(data.port, 2352);
        console.log('case9\tSuccessful');
        case10();
    });
}

// Try looking up doa1 again -- still should exit, because registering another name should not have effected the doa1
// registration
function case10() {
    Client.Lookup('doa1', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data.name, 'doa1');
        assert.equal(data.type, 'doa');
        assert.equal(data.host, '129.123.7.22');
        assert.equal(data.port, 3332);
        console.log('case10\tSuccessful');
        case11();
    });
}

// Trying looking up another non-existing name
function case11() {
    Client.Lookup('emr3', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case11\tSuccessful');
        case12();
    });
}

// Try registering an null name -- shouldn't work
function case12() {
    Client.Register(null, 'emr', '54.23.5.23', 2352, function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case12\tSuccessful');
        case13();
    });
}

// Try registering something with a null type -- shouldn't work
function case13() {
    Client.Register('emr3', null, '54.23.5.23', 2352, function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case13\tSuccessful');
        case14();
    });
}

// Try registering something with a null IP Address -- shouldn't work
function case14() {
    Client.Register('emr1', 'emr', null, 2352, function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case14\tSuccessful');
        case15();
    });
}

// Try registering something with a null port -- shouldn't work
function case15() {
    Client.Register('emr1', 'emr', '54.23.5.23', null, function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, false);
        console.log('case15\tSuccessful');
        case16();
    });
}

// Try getting all entries
function case16() {
    Client.All(function (error, data) {
        assert.equal(error === null, true);

        var entry = data['hds2'];
        assert.equal(entry.name, 'hds2');
        assert.equal(entry.host, '54.23.5.23');
        assert.equal(entry.port, 2352);

        entry = data['doa1'];
        assert.equal(entry.name, 'doa1');
        assert.equal(entry.type, 'doa');
        assert.equal(entry.host, '129.123.7.22');
        assert.equal(entry.port, 3332);

        console.log('case16\tSuccessful');

        case17();
    });
}

// Make sure emr9 is not registered
function case17() {
    Client.Deregister('emr9', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, true);
        console.log('case17\tSuccessful');
        case18();
    });
}

// Try to lookup a name that isn't registered yet.  It should retry and eventually get find it, because we will
// register it right after starting the lookup
function case18()
{
    Client.Lookup('emr9', function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data.name, 'emr9');
        assert.equal(data.type, 'emr');
        assert.equal(data.host, '99.23.5.23');
        assert.equal(data.port, 2359);
        console.log('case18\tSuccessful');
    });

    Client.Register('emr9', 'emr', '99.23.5.23', 2359, function (error, data) {
        assert.equal(error === null, true);
        assert.equal(data, true);
    });

}
