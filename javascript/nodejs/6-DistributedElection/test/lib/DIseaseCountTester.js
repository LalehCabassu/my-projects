var assert = require('assert');

VectorTimestamp = require("./../../lib/VectorTimestamp.js");
DiseaseCount = require("./../../lib/DiseaseCount.js");

var t1 = new VectorTimestamp(15);
var dc1 = new DiseaseCount(2, 3, 45, t1);

assert.equal(dc1.constructor.name, 'DiseaseCount');
assert.equal(dc1.district, 2);
assert.equal(dc1.disease, 3);
assert.equal(dc1.delta, 45);

assert.equal(dc1.timestamp.constructor.name, "VectorTimestamp");
assert.equal(dc1.timestamp.size, 15);

var dc1json = dc1.toJSON();
var dc2 = DiseaseCount.fromJSON(dc1json);
assert.equal(dc2.constructor.name, 'DiseaseCount');
assert.equal(dc2.district, dc1.district);
assert.equal(dc2.disease, dc1.disease);
assert.equal(dc2.delta, dc1.delta);

assert.equal(dc2.timestamp.constructor.name, "VectorTimestamp");
assert.equal(dc2.timestamp.size, dc1.timestamp.size);

console.log('Success');