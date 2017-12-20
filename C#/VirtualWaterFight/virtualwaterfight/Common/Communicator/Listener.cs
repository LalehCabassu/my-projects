using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using log4net.Config;

using Common.Threading;
using Common.Messages;


namespace Common.Communicator
{
    class Listener : BackgroundThread
    {
        #region Data members and Getter/Setter
        private static readonly ILog log = LogManager.GetLogger(typeof(Listener));
        private MessageQueue myQueue;
        #endregion

        #region Methods
        public Listener(MessageQueue queue)
        {  
            myQueue = queue;
        }

        public override string ThreadName()
        {
            return "Listener";
        }

        public void putInQueue(Envelope newEnvelope)
        {
            if (newEnvelope != null)
            {
                log.InfoFormat("putInQueue", ThreadName());
                myQueue.Enqueue(newEnvelope);
                newEnvelope = null;
            }
        }
        #endregion

        #region Private Methods
        
          protected override void Process()
        {
            while (keepGoing)
            {
                if (!suspended)
            //        putInQueue();
                    
                Thread.Sleep(0);
            }
        }
         
        #endregion

    }
}
