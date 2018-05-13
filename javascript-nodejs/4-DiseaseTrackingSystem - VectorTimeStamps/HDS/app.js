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

// where to use this????
//var diseases = require('./../lib/Diseases.js');
var NotificationGenerator = require('./NotificationGenerator.js');

// TODO: require your vector timestamp module
// TODO: require your timestamp logger
var VectorTimestamp = require('./../lib/VectorTimestamp.js');
var tsLogger = require('./../lib/TimestampLogger.js');

// Initialization
// TODO: Setup local process id
if(process.argv.length >= 4)
    process.myProcessId = Number(process.argv[3]);
else
    process.myProcessId = -1;

// TODO: Setup up timestamp log
tsLogger.SetupLogFile(process.myProcessId);

// TODO: Log initial timestamp
VectorTimestamp.Init(15);
var timestamp = VectorTimestamp.LocalTimestamp();
tsLogger.LogProcessTimestamp(process.myProcessId, timestamp);

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

console.log(process.argv);

if (process.argv.length >= 5) {
    var generator = new NotificationGenerator(2000);
    generator.Run();

    setTimeout(function () {
        generator.Stop();
    }, parseInt(process.argv[4]));
}
else
{
    console.log("No timeout provided");
}

module.exports = app;
