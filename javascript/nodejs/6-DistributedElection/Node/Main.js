var app = require('./app.js');
var NameServerClient = require('../NS/NameServerClient.js');
var Worker = require('./Worker.js');
var EM = require('./ElectionManager.js');
var RM = require('./ResourceManager.js');

var port;
var server;
var forcefullShutdownTimeout = 10000;

function onError(error)
{
  if (error.syscall !== 'listen')
    throw error;

  // handle specific listen errors with friendly messages
  switch (error.code) {
    case 'EACCES':
      console.log('Port ' + port + ' requires elevated privileges');
      process.exit(1);
      break;
    case 'EADDRINUSE':
      console.log('Port ' + port + ' is already in use');
      process.exit(1);
      break;
    default:
      throw error;
  }
}

function onListening()
{
  var address = server.address();
  console.log('listening on ' + address.address + ':' + address.port);

  // Make sure the port is set in the app object.
  app.set('port', server.address().port);

  process.myType = 'node';
  process.myAddress = address;
  NameServerClient.RegisterSelf(function (error, data) {
    if (error != null)
      console.log('Cannot register with Name Server');
    else {
      process.isRegistered = true;
      process.myName = data.name;
      process.myAddress.host = data.host;
      process.myProcessId = data.processId;
      console.log('Registered with Name Server as process ' + process.myName + ' (' + process.myProcessId + ') at ' + process.myAddress.host + ':' + process.myAddress.port);

      console.log('Initialize Resource Manager');
      RM.SetActiveRM(null);

      console.log('Starting up the Election Manager')
      EM.Run();

      console.log('Starting the Worker');
      Worker.Run();

    }
  });
}

function gracefulShutdown()
{
  console.log("Received kill signal, shutting down gracefully.");

  Worker.Stop();
  EM.Stop();

  if (process.myName != undefined && process.myName != null)
    NameServerClient.DeregisterSelf(function(error, data)
    {
      if (error!=null && data==true)
        console.log('Cannot de-register self from Name Server');
      else
      {
        process.myName = null;
        console.log('Deregistered self from Name Server');
      }
    });

  server.close(function() {
    console.log("Closed out remaining connections.");
    process.exit();
  });

  var forceExitTimer = setTimeout(function() {
    console.error("Could not close connections in time, forcefully shutting down");
    process.exit();
  }, forcefullShutdownTimeout);
  forceExitTimer.unref();
}

function main()
{
  // Get port and store in Express.
  port = process.env.PORT || parseInt(process.argv[2]) || 0;

  // Setup NameServerClient
  var nsHost = process.argv[3];
  var nsPort = parseInt(process.argv[4]) | 2499;
  NameServerClient.Setup(nsHost, nsPort);

  // Create HTTP server.
  server = app.listen(port);

  // Setup event handlers
  server.on('error', onError);
  server.on('listening', onListening);
  process.on('SIGTERM', gracefulShutdown);
  process.on('SIGINT', gracefulShutdown);

  // Setup RM
  RM.SetLocalInputFolder('./Input/');
  RM.SetLocalOutputFolder('./Output/');
  RM.SetRemoteBucket('swcmain');
  RM.SetRemoteInputFolder('cs6200hw4/input/');
  RM.SetRemoteOutputFolder('cs6200hw4/output/');
}

main();