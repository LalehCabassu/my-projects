var assert = require('assert');

var VectorTimestamp = require("./../../lib/VectorTimestamp.js");

var t1 = new VectorTimestamp(15);
assert.equal(t1.constructor.name, "VectorTimestamp");
assert.equal(t1.size, 15);
for (i=0; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'initial vectors must equal 0');

t1.Increment(0);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
for (i=1; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'Element ' + i.toString() + ' must equal 0');

t1.Increment(1);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 1, 'Element 1 must be a 1');
for (i=2; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'Element ' + i.toString() + ' must equal 0');

t1.Increment(1);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 2, 'Element 1 must be a 2');
for (i=2; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'Element ' + i.toString() + ' must equal 0');

t1.Increment(1);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 3, 'Element 1 must be a 3');
for (i=2; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'Element ' + i.toString() + ' must equal 0');

t1.Increment(2);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 3, 'Element 1 must be a 3');
assert.equal(t1.Element(2), 1, 'Element 2 must be a 1');
for (i=3; i < t1.size; i++)
    assert.equal(t1.Element(i), 0, 'Element ' + i.toString() + ' must equal 0');

for (i=3; i < t1.size; i++)
    t1.Increment(i);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 3, 'Element 1 must be a 3');
for (i=2; i < t1.size; i++)
    assert.equal(t1.Element(i), 1, 'Element ' + i.toString() + ' must equal 1');

t1.Increment(t1.size-1);
t1.Increment(t1.size-1);
t1.Increment(t1.size-1);
assert.equal(t1.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t1.Element(1), 3, 'Element 1 must be a 3');
assert.equal(t1.Element(2), 1, 'Element 2 must be a 1');
assert.equal(t1.Element(t1.size-1), 4, 'The last element must be a 4');
for (i=2; i < t1.size - 1; i++)
    assert.equal(t1.Element(i), 1, 'Element ' + i.toString() + ' must equal 1');

var t1json = t1.toJSON();
var t2 = VectorTimestamp.fromJSON(t1json);
assert.equal(t2.constructor.name, "VectorTimestamp");
assert.equal(t2.size, 15);
assert.equal(t2.Element(0), 1, 'Element 0 must be a 1');
assert.equal(t2.Element(1), 3, 'Element 1 must be a 3');
assert.equal(t2.Element(2), 1, 'Element 2 must be a 1');
assert.equal(t2.Element(t2.size-1), 4, 'The last element must be a 4');
for (i=2; i < t2.size - 1; i++)
    assert.equal(t2.Element(i), 1, 'Element ' + i.toString() + ' must equal 1');

console.log('Success');
