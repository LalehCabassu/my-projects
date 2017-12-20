/**
 * Created by Life on 4/28/15.
 */

var assert = require('assert');
var tmClient = require('./../../TransactionManager/TransactionManagerClient.js');
var rmClient = require('./../../ResourceManager/ResourceManagerClient.js');


var tmhost = process.argv[2];
var tmport = parseInt(process.argv[3]);
tmClient.Setup(tmhost, tmport);                   // Setup the host and port number for the client

var rmhost = process.argv[4];
var rmport = parseInt(process.argv[5]);
rmClient.Setup(rmhost, rmport);

// Setup the host and port number for the client
var tranId = 0;
case1();

// Try registering first process
function case0() {
    tmClient.Register('RM01', rmhost, rmport, function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data, true);
        console.log('case0\tSuccessful');
        case1();
    });
}

function case1() {
    tmClient.Start(function (error, data) {
        console.log('data: ' + data);
        assert.equal(error == null, true);
        assert.equal(data != null, true);
        tranId = data;
        console.log('case1\tSuccessful');
        case2();
        //case3();
        //case4();
    });
}

function case2() {
    rmClient.Lock(tranId, 'write', 500, function(err, data){
        if(data == true)
            rmClient.ToUpper(tranId, function(err, data){
                if(data == true) {
                    console.log("Upperred resource.");
                    tmClient.Commit(tranId, ['RM01'], function (error, data) {
                        console.log('data: ' + data);
                        assert.equal(error == null, true);
                        assert.equal(data, true);
                        rmClient.Lock(tranId, 'read', 500, function (error, data) {
                            console.log('data: ' + data);
                            rmClient.Read(tranId, function (error, data) {
                                console.log('data: ' + data);
                                assert.equal(error == null, true);
                                assert.equal(data, 'LALEH');
                                console.log('case2\tSuccessful');
                            });
                        });
                    });
                }
                else
                    console.log("Error in uppering the value.");
            });
        else
            console.log("Error in acquiring a write lock.");
    });
}

function case3() {
    rmClient.Lock(tranId, 'write', 500, function(err, data){
        if(data == true)
            rmClient.ToLower(tranId, function(err, data){
                if(data == true)
                {
                    console.log("Lowered resource.");
                    tmClient.Abort(tranId, ['RM01'], function (error, data) {
                        console.log('data: ' + data);
                        assert.equal(error == null, true);
                        assert.equal(data, true);
                        rmClient.Lock(tranId, 'read', 500, function (error, data) {
                            console.log('data: ' + data);
                            rmClient.Read(tranId, function (error, data) {
                                console.log('data: ' + data);
                                assert.equal(error == null, true);
                                assert.equal(data, 'Laleh');
                                console.log('case3\tSuccessful');
                            });
                        });
                    });
                }
                else
                    console.log("Error in lowering the value.");
            });
        else
            console.log("Error in acquiring write lock.");
    });
}

function case4() {

    rmClient.Lock(tranId, 'read', 500, function(err, data){
        if(data == true)
            rmClient.Lock(tranId, 'write', function(err, data){
                if(data == true)
                {
                    console.log("Lowered resource.");

                    var rmhost = process.argv[6];
                    var rmport = parseInt(process.argv[7]);
                    rmClient.Setup(rmhost, rmport);

                    rmClient.Lock(tranId, 'read', 500, function(err, data){
                        if(data == true)
                            rmClient.Lock(tranId, 'write', function(err, data){
                                if(data == true)
                                {
                                    tmClient.Commit(tranId, ['RM01'], function (error, data) {
                                    console.log('data: ' + data);
                                    assert.equal(error == null, true);
                                    assert.equal(data, true);
                                    rmClient.Lock(tranId, 'read', 500, function (error, data) {
                                        console.log('data: ' + data);
                                        rmClient.Read(tranId, function (error, data) {
                                            console.log('data: ' + data);
                                            assert.equal(error == null, true);
                                            assert.equal(data, 'Laleh');
                                            console.log('case3\tSuccessful');
                                        });
                                    });
                                });
                            }
                        });
                    });
                }
                else
                    console.log("Error in lowering the value.");
            });
        else
            console.log("Error in acquiring write lock.");
    });
}