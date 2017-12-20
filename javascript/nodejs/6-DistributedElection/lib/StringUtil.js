
module.exports = {
    GetDateTime: function () {
        var date = new Date();

        var hour = date.getHours();
        hour = (hour < 10 ? "0" : "") + hour;

        var min  = date.getMinutes();
        min = (min < 10 ? "0" : "") + min;

        var sec  = date.getSeconds();
        sec = (sec < 10 ? "0" : "") + sec;

        return hour + ":" + min + ":" + sec;
    },

    NumberPadder: function(num, length)
    {
        var result = '';

        for(var i = 0; i < length ; i++)
            result += '0';
        result = String(result + num).slice(-length);
        return result;
    }

}