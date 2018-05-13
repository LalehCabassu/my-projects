
// Constructor
function AmAliveGenerator(startRate, maxRate, rateChange)
{
    this.startRate = 1000 / startRate;
    this.maxRate = 1000 / maxRate;
    this.rateChange = rateChange;
    this.currentRate = 0;
    this.timer = null;
    this.running = false;
}

// post is a callback passed by Simulator, called PpstCode
AmAliveGenerator.prototype.Run = function(post)
{
    this.running = true;
    this.currentRate = this.startRate;
    this.post = post;

    /*
     To schedule execution of a one-time callback after delay milliseconds.
     Returns a timeoutObject for possible use with clearTimeout().
     Optionally you can also pass arguments to the callback.
     */
    this.generationTimer = setTimeout(GenerateAmAlive, this.currentRate, this);
    this.generationTimer.generator = this;

    /*
     To schedule the repeated execution of callback every delay milliseconds.
     Returns a intervalObject for possible use with clearInterval().
     Optionally you can also pass arguments to the callback.
     */
    this.rateChangeTimer = setInterval(ChangeRate, 1000, this);
    this.rateChangeTimer.generator = this;
}

AmAliveGenerator.prototype.Stop = function()
{
    if (this.running)
    {
        clearTimeout(this.generationTimer);
        clearInterval(this.rateChangeTimer);
        this.running = false;
        this.generationTimer = 0;
    }
}

var GenerateAmAlive = function(generator)
{
    if (generator.running)
    {
        if (generator.post != null)
        {
            generator.post();
        }

        generator.generationTimer = setTimeout(GenerateAmAlive, generator.currentRate, generator);
    }
}

var ChangeRate = function(generator)
{
    if (generator.running)
        generator.currentRate = Math.max(generator.maxRate, generator.currentRate - generator.rateChange);
}

module.exports = AmAliveGenerator;
