using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// This class represents a player's session.  It is a singleton.
    /// Its one instance is created on successful login and deleted
    /// on logout.
    /// </summary>
    public class Session
    {
        private static Session myInstance = null;
        private Int16 sessionId = 0;

        public Session(Int16 sessionId)
        {
            this.sessionId = sessionId;
        }

        public void Destroy()
        {
            myInstance = null;
        }

        public static Int16 SessionId
        {
            get
            {
                Int16 result = 0;
                if (myInstance != null)
                    result = myInstance.sessionId;
                return result;
            }
        }


    }
}
