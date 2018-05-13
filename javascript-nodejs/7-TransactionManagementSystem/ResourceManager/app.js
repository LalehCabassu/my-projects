var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var lock = require('./routes/lock');
var read = require('./routes/read');
var write = require('./routes/write');
var concatenate = require('./routes/concatenate');
var truncate = require('./routes/truncate');
var substring = require('./routes/substring');
var toupper = require('./routes/toupper');
var tolower = require('./routes/tolower');
var unlock = require('./routes/unlock');
var rollback = require('./routes/rollback');

var nameServerClient = require('./../NS/NameServerClient.js');
var transactionManagerClient = require('./../TransactionManager/TransactionManagerClient.js');
var resourceManager = require('./ResourceManager.js');

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

app.use('/lock', lock);
app.use('/read', read);
app.use('/write', write);
app.use('/concatenate', concatenate);
app.use('/truncate', truncate);
app.use('/substring', substring);
app.use('/tolower', tolower);
app.use('/toupper', toupper);
app.use('/unlock', unlock);
app.use('/rollback', rollback);

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
    if (process.argv.length >= 10) {

        process.name = process.argv[2];
        var ipAddress = '127.0.0.1';
        var port = process.argv[3];

        var resource = process.argv[4];
        var resourceValue = process.argv[5];

        // Setup name server endpoint
        var nshost = process.argv[6];
        var nsport = parseInt(process.argv[7]);
        nameServerClient.Setup(nshost, nsport);

        // Setup transaction manager endpoint
        var tmhost = process.argv[8];
        var tmport = parseInt(process.argv[9]);
        transactionManagerClient.Setup(tmhost, tmport)

        // Register to the name server
        nameServerClient.Register(process.name, ipAddress, port, function (error, data) {
            if (error != null || data == false)
                console.log('{' + ipAddress + ':' + port + '} -> Failed in registering to the nameserver! Name: ' + process.name);
            else {
                console.log('{' + ipAddress + ':' + port + '} -> Successfully registered. Name: ' + process.name);

                nameServerClient.RegisterResourceManager(process.name, resource, function (error, data) {
                        if (error != null || data == false)
                            console.log('{' + ipAddress + ':' + port + '} -> Failed in registering the resource manager to the nameserver! Name: ' + process.name);
                        else {
                            console.log('{' + ipAddress + ':' + port + '} -> The resource manager successfully registered to the nameserver. Name: ' + process.name);

                            // register to transaction manager
                            transactionManagerClient.Register(process.name, ipAddress, port, function (error, data) {
                                if (error != null || data == false)
                                    console.log('{' + ipAddress + ':' + port + '} -> Failed in registering the resource manager to the transaction manager! Name: ' + process.name);
                                else {
                                    console.log('{' + ipAddress + ':' + port + '} -> The resource manager successfully registered to the transaction manager. Name: ' + process.name);
                                    resourceManager.Active(resource, resourceValue);
                                    console.log('The resource manager is activated...');
                                }
                            });
                        }
                    });
            }
        });
    }
}

main();

module.exports = app;
