var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
//var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var amalive = require('./routes/amalive');
var elected = require('./routes/elected');
var election = require('./routes/election');
var getstringpair = require('./routes/getstringpair');
var saveresult = require('./routes/saveresult');

var nameServerClient = require('./../NS/NameServerClient.js');
var networkInterfaces = require('./../lib/NetworkInterfaces.js');
var resourceManager = require('./ResourceManager.js');
var worker = require('./Worker.js');
var electionManager = require('./ElectionManager.js');

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

app.use('/amalive', amalive);
app.use('/election', election);
app.use('/elected', elected);
app.use('/getstringpair', getstringpair);
app.use('/saveresult', saveresult);

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
    if (process.argv.length >= 5) {

        var publicAddress = networkInterfaces.GetBestPublicAddress();
        var port = process.argv[2];

        // Setup name server endpoint
        var nshost = process.argv[3];
        var nsport = parseInt(process.argv[4]);
        nameServerClient.Setup(nshost, nsport);

        // Register to the name server
        nameServerClient.Register(publicAddress, port, function (error, data) {
            if (error != null)
                console.log('{' + publicAddress + ':' + port + '} -> Failed in registering to the nameserver!');
            else {
                process.processId = data.processId;
                console.log('{' + publicAddress + ':' + port + '} -> Successfully registered. ProcessId: ' + process.processId);

                electionManager.Active();

                //resourceManager.Active(function(err, data){
                //    if(err){
                //        console.log("Failed to start the resource manager. Error: ", err);
                //    }
                //    else{
                //        console.log("Resource manager is running.");
                //        worker();
                //    }
                //});
            }
        });
    }
}

main();

module.exports = app;
