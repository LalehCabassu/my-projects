var Notification = require('./../lib/Notification.js');
var diseases = require('./../lib/Diseases.js');
var diseaseCounts = require('./DiseaseCounts.js');
var VectorTimestamp = require('./../lib/VectorTimestamp.js');

// Constructor
function NotificationGenerator(startRate, maxRate, rateChange)
{
    this.startRate = 1000 / startRate;
    this.maxRate = 1000 / maxRate;
    this.rateChange = rateChange;
    this.currentRate = 0;
    this.timer = null;
    this.running = false;
}

// post is a callback passed by Simulator, called PpstCode
NotificationGenerator.prototype.Run = function(post)
{
    this.running = true;
    this.currentRate = this.startRate;
    this.post = post;

    /*
     To schedule execution of a one-time callback after delay milliseconds.
     Returns a timeoutObject for possible use with clearTimeout().
     Optionally you can also pass arguments to the callback.
     */
    this.generationTimer = setTimeout(GenerateNotification, this.currentRate, this);
    this.generationTimer.generator = this;

    /*
     To schedule the repeated execution of callback every delay milliseconds.
     Returns a intervalObject for possible use with clearInterval().
     Optionally you can also pass arguments to the callback.
     */
    this.rateChangeTimer = setInterval(ChangeRate, 1000, this);
    this.rateChangeTimer.generator = this;
}

NotificationGenerator.prototype.Stop = function()
{
    if (this.running)
    {
        clearTimeout(this.generationTimer);
        clearInterval(this.rateChangeTimer);
        this.running = false;
        this.generationTimer = 0;
    }
}

var GenerateNotification = function(generator)
{
    if (generator.running)
    {
        var d = diseases.SelectRandom();
        var n = new Notification(process.myProcessId, d, VectorTimestamp.LocalTimestamp());
        if (generator.post != null)
        {
            // console.log('generate a notification for ' + generator.diseases.DISEASE_NAMES[n.disease]);
            generator.post(n);
            diseaseCounts.IncrementCount(d);
        }

        generator.generationTimer = setTimeout(GenerateNotification, generator.currentRate, generator);
    }
}

var ChangeRate = function(generator)
{
    if (generator.running)
        generator.currentRate = Math.max(generator.maxRate, generator.currentRate - generator.rateChange);
}

module.exports = NotificationGenerator;
