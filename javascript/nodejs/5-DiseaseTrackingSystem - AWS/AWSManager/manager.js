/**
 * Created by Life on 2/9/15.
 */

var startInstances = require('./StartInstances.js');
var stopInstances = require('./StopInstances.js');
var deploy = require('./Deploy.js');
var run = require('./Run.js');

//To launch the whole system
//startInstances.Start(deploy.Do, run.Do);

deploy.Do();

//run.Do();

//To stop instances
//startInstances.ListInstances(stopInstances.Do);

