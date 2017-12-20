var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var routes = require('./routes/index');
var alerts = require('./routes/alerts');
var notify = require('./routes/notify');

var app = express();

var NotificationGenerator = require('./NotificationGenerator.js');

// TODO: require your vector timestamp module
// TODO: require your timestamp logger
var VectorTimestamp = require('../lib/VectorTimestamp.js');
var tsLogger = require('../lib/TimestampLogger.js');

var NetworkInterfaces = require('../lib/NetworkInterfaces.js');
var NameServerClient = require('../NS/NameServerClient.js');

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

// uncomment after placing your favicon in /public
//app.use(favicon(__dirname + '/public/favicon.ico'));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', routes);
app.use('/alerts', alerts);
app.use('/notify', notify);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function(err, req, res, next) {
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});

function main()
{
    // Initialization
    // TODO: Setup local process id
    if(process.argv.length >= 7) {
        process.myProcessId = parseInt(process.argv[3]);
        process.myName = 'hds' + process.myProcessId.toString();

        // Setup name server endpoint
        var nshost = process.argv[5];
        var nsport = parseInt(process.argv[6]);
        NameServerClient.Setup(nshost, nsport);

        // Register to the name server
        var publicAddress = NetworkInterfaces.GetBestPublicAddress();
        var port = process.argv[2];
        NameServerClient.Register(process.myName, 'hds', publicAddress, port, function(error, data)
        {
            if (error!=null)
                console.log('Error in registering ' + process.myName + ' with the Name Server, error=' + error);
            else
                console.log('Registered ' + process.myName + ' as ' + publicAddress + ':' + port);
        });

        // TODO: Setup up timestamp log
        tsLogger.SetupLogFile(process.myProcessId);

        // TODO: Log initial timestamp
        VectorTimestamp.Init(15);
        var timestamp = VectorTimestamp.LocalTimestamp();
        tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

        var generator = new NotificationGenerator(2000);
        generator.Run();

        setTimeout(function () {
            generator.Stop();
        }, parseInt(process.argv[4]));

    }
    else
        console.log("Argument missing");
}

main();

module.exports = app;
