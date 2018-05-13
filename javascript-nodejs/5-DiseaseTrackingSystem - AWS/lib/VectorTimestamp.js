var stringUtil = require('./StringUtil');
// TODO: Declare a private local timestamp ???
var localTimestamp;

// TODO: Declare  VectorTimestamp class, with
//  Constructor
//      setup a vector of a specified size
function VectorTimestamp(size) {
    this.elements = new Array(size);
    this.size = size;
    for(var i = 0; i < size; i++)
        this.elements[i] = 0;
}

// TODO: Methods
//      increment method, give a process id
VectorTimestamp.prototype.Increment = function(index)
{
    this.elements[index]++;
}

//      merge method
VectorTimestamp.prototype.Merge = function(otherTS)
{
    for(var i = 0; i < this.elements.length && i < otherTS.size; i++)
        if(otherTS.Element(i) > this.elements[i])
            this.elements[i] = otherTS.Element(i);
}

//      get a element
VectorTimestamp.prototype.Element = function(index)
{
    return this.elements[index];
}

VectorTimestamp.prototype.ToString = function()
{
    var result = '[ ';

    for(var i = 0; i<this.elements.length; i++)
    {
        result += stringUtil.NumberPadder(this.elements[i], 5) + ' ';
    }
    result += ']';
    return result;
}

//      get a json serialization of this timestamp (toJSON)
VectorTimestamp.prototype.toJSON = function()
{
    return JSON.stringify({
        _type: this.constructor.name,
        elements: this.elements,
        size: this.size
    });
}


// TODO: Declare a function to return timestamp from a json string (fromJSON)
VectorTimestamp.fromJSON = function(json)
{
    var obj = JSON.parse(json);
    if (obj._type != 'VectorTimestamp')
        throw 'json string does not contain a vector timestamp';

    var newVectorTimestamp = new VectorTimestamp(obj.size);
    newVectorTimestamp.elements = obj.elements;

    return newVectorTimestamp;
}

// TODO: Declare a function to setup a the local timestamp
// ?.Init(size???) -> initialize the array of timestamp
VectorTimestamp.Init = function(size)
{
    localTimestamp = new VectorTimestamp(size);
}

// TODO: Declare a function to return the local timestamp
VectorTimestamp.LocalTimestamp = function()
{
    return localTimestamp;
}

// TODO: Declare a function to return a string of the timestamp for display and logging processes


// TODO: export the constructor
module.exports = VectorTimestamp;