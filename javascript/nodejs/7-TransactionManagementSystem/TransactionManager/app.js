var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
//var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var register = require('./routes/register');
var start = require('./routes/start');
var commit = require('./routes/commit');
var abort = require('./routes/abort');

var nameServerClient = require('./../NS/NameServerClient.js');
//var networkInterfaces = require('./../lib/NetworkInterfaces.js');
var TransactionManager = require('./TransactionManager.js');

var app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

// uncomment after placing your favicon in /public
//app.use(favicon(__dirname + '/public/favicon.ico'));
//app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/register', register);
app.use('/start', start);
app.use('/commit', commit);
app.use('/abort', abort);

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

function main() {
    if (process.argv.length >= 6) {

        process.name = process.argv[2];

        var ipAddress = '127.0.0.1';
        var port = process.argv[3];

        // Setup name server endpoint
        var nshost = process.argv[4];
        var nsport = parseInt(process.argv[5]);
        nameServerClient.Setup(nshost, nsport);

        // Register to the name server
        nameServerClient.Register(process.name, ipAddress, port, function (error, data) {
            if (error != null)
                console.log('{' + ipAddress + ':' + port + '} -> Failed in registering to the nameserver!');
            else {
                console.log('{' + ipAddress + ':' + port + '} -> Successfully registered. Name: ' + process.name);

                TransactionManager.Active();
                console.log("Transaction manager is activated...");
            }
        });
    }
}

main();

module.exports = app;
