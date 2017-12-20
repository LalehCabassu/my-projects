module.exports =
{
    DISEASE_NAMES: ['Unknown', 'Flu', 'Chicken Pox', 'Measles'],
    UNKNOWN: 0,
    FLU: 1,
    CHICKEN_POX: 2,
    MEASLES: 3,
    COUNT: 4,

    SelectRandom: function()
    {
        return Math.floor(0 + Math.random()*3);

        // never returns 0: Unknown
        //return Math.floor(1 + Math.random()*3);
    }
};