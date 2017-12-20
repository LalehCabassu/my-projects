/**
 * Created by Life on 4/14/15.
 */


function Transaction(id)
{
    this.id = id;
    this.resourceManagers = [];
    this.startTimeStamp = Date.now();
    this.alive = true;
    this.killingCounter = 0;
}

module.exports = Transaction;