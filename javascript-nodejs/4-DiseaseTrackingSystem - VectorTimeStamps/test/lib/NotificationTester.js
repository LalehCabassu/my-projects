var assert = require('assert');

VectorTimestamp = require("./../../lib/VectorTimestamp.js");
Notification = require("./../../lib/Notification.js");

var t1 = new VectorTimestamp(15);
var n1 = new Notification(2, 3, t1);

assert.equal(n1.constructor.name, 'Notification');
assert.equal(n1.emr, 2);
assert.equal(n1.disease, 3);
assert.ok(n1.reportedOn <= new Date());

assert.equal(n1.timestamp.constructor.name, "VectorTimestamp");
assert.equal(n1.timestamp.size, 15);

var n1json = n1.toJSON();
var n2 = Notification.fromJSON(n1json);
assert.equal(n2.constructor.name, 'Notification');
assert.equal(n2.emr, n1.emr);
assert.equal(n2.disease, n1.disease);
assert.equal(n2.reportedOn.toString(), n1.reportedOn.toString());

assert.equal(n2.timestamp.constructor.name, "VectorTimestamp");
assert.equal(n2.timestamp.size, n1.timestamp.size);

console.log('Success');