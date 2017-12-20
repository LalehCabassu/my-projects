var assert = require('assert');

var constants = require('./../../lib/Diseases.js');

console.log(constants.DISEASE_NAMES);
assert.equal(constants.UNKNOWN,0);
assert.equal(constants.FLU,1);
assert.equal(constants.CHICKEN_POX,2);
assert.equal(constants.MEASLES,3);

console.log('Success');