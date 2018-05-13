var assert = require('assert');
var RM = require('../../Node/ResourceManager.js');

var numberOfTestFiles = 6;
var linesPerFile = 100;
var lineCount = 0;

RM.SetLocalInputFolder('./Input/');
RM.SetLocalOutputFolder('./Output/');
RM.SetRemoteBucket('swcmain');
RM.SetRemoteInputFolder('cs6200hw4/input/');
RM.SetRemoteOutputFolder('cs6200hw4/output/');

RM.ClearState(function(error, data) {
    if (error!=null) console.log(error);
    else
    {
        RM.BecomeActiveRM();
        Test00();
    }
});

function Test00()
{
    var rmStatus = RM.Status();
    assert.equal(rmStatus!=null & rmStatus!=undefined, true);
    assert.equal(rmStatus.currentActiveResourceManager, 'self');
    assert.equal(rmStatus.needsInitialization, true);
    assert.equal(rmStatus.inputExhausted, false);
    Test01();
}

function Test01()
{
    RM.GetStringPair(function (error, data) {
        if (error != null)
            console.log(error);
        else
        {
            assert.equal(data!=null, true);
            assert.equal(data.length, 3);
            lineCount++;
            Test02();
        }
    });
}

function Test02()
{
    RM.GetStringPair(function (error, data) {
        if (error=='EOF')
        {
            assert.equal(lineCount, numberOfTestFiles*linesPerFile);
            Test03();
        }
        else if (error != null)
            console.log(error);
        else
        {
            console.log('Read pair ' + lineCount + ' : ' + data[0]);
            assert.equal(data!=null, true);
            assert.equal(data.length, 3);

            var recordNumber = parseInt(data[0]);
            var predicatedNumber = lineCount % 100;
            assert.equal(recordNumber, predicatedNumber);

            lineCount++;
            Test02();
        }
    });
}

function Test03()
{
    RM.ClearState(function(error, data) {
        if (error!=null) console.log(error);
        else
        {
            lineCount = 0;
            Test04();
        }
    });

}

function Test04()
{
    RM.GetStringPair(function (error, data)
    {
        if (error=='EOF')
        {
            assert.equal(lineCount, numberOfTestFiles*linesPerFile);
            Test99();
        }
        else if (error != null)
            console.log(error);
        else
        {
            console.log('Read pair ' + lineCount + ' : ' + data[0]);
            assert.equal(data!=null, true);
            assert.equal(data.length, 3);

            var recordNumber = parseInt(data[0]);
            var predicatedNumber = lineCount % 100;
            assert.equal(recordNumber, predicatedNumber);

            lineCount++;
            if ((lineCount % 150) != 0)
                Test04();
            else
            {
                console.log('Stop being the active RM');
                RM.SetActiveRM(null);
                console.log('Start being the active RM');
                RM.BecomeActiveRM();
                Test04();
            }
        }
    });
}

function Test99()
{
    RM.SetActiveRM(null);
}
