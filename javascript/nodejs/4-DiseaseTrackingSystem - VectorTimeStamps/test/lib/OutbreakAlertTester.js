var assert = require('assert');

VectorTimestamp = require("./../../lib/VectorTimestamp.js");
OutbreakAlert = require("./../../lib/OutbreakAlert.js");

var t1 = new VectorTimestamp(15);
var a1 = new OutbreakAlert(2, 3, t1);

assert.equal(a1.constructor.name, 'OutbreakAlert');
assert.equal(a1.district, 2);
assert.equal(a1.disease, 3);
assert.ok(a1.alertDate <= new Date());

assert.equal(a1.timestamp.constructor.name, "VectorTimestamp");
assert.equal(a1.timestamp.size, 15);

var a1json = a1.toJSON();
var a2 = OutbreakAlert.fromJSON(a1json);
assert.equal(a2.constructor.name, 'OutbreakAlert');
assert.equal(a2.district, a1.district);
assert.equal(a2.disease, a1.disease);
assert.equal(a2.alertDate.toString(), a1.alertDate.toString());

assert.equal(a2.timestamp.constructor.name, "VectorTimestamp");
assert.equal(a2.timestamp.size, a1.timestamp.size);

console.log('Success');