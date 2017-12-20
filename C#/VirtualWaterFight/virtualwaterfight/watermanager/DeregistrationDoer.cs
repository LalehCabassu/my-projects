using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Common;
using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace WaterManager
{
    public class DeregistrationDoer : Doer
    {
        #region Data Members Getter/Setter
        
        private AckNak incomingAckNak;
        private WaterManager MyWaterManager;
        #endregion

        #region Public Methods
        
        public DeregistrationDoer(Communicator communicator, WaterManager myWaterManager)
            : base(communicator)
        {
            MyWaterManager = myWaterManager;
        }

        public override void DoProtocol(Envelope message)
        {
            incomingAckNak = (AckNak)message.Message;

            switch (incomingAckNak.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyWaterManager.RemovePlayer(incomingAckNak.ConversationId.ProcessId);
                    break;
                case Reply.PossibleStatus.InvalidLocation:
                    // TODO: Notify player to choose another location
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
                            
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
