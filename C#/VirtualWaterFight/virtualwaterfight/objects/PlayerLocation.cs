using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class PlayerLocation
    {
        public Location Location;
        public DateTime AtTime;

        public PlayerLocation(Location location)
        {
            Location = location;
            AtTime = DateTime.Now;
        }

        public PlayerLocation(Location location, DateTime time)
        {
            Location = location;
            AtTime = time;
        }
    }
}
