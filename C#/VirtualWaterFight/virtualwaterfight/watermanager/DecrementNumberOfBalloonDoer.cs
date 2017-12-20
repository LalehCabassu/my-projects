using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Common;
using Common.Threading;
using Common.Messages;
using Common.Communicator;

namespace WaterManager
{
    public class DecrementNumberOfBalloonDoer : Doer
    {
        #region Data Members Getter/Setter
        private DecrementNumberOfBalloonsRequest incomingRequest;
        private WaterManager MyWaterManager;
        #endregion

        #region Public Methods
        
        public DecrementNumberOfBalloonDoer(Communicator communicator, WaterManager myWaterManager)
            : base(communicator)
        {
            MyWaterManager = myWaterManager;
        }

        public override void DoProtocol(Envelope message)
        {
            incomingRequest = (DecrementNumberOfBalloonsRequest)message.Message;
            MyWaterManager.RemoveBalloon(incomingRequest.PlayerID, incomingRequest.BalloonID);
        }
        #endregion

        #region Private Methods
        protected override void Process()
        {
            while (keepGoing)
            {/*
                if (!suspended)
                {
                }
              */ 
                Thread.Sleep(0);
            }
        }
        #endregion
    }
}
