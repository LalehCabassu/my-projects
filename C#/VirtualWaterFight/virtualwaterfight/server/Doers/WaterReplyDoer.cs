using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace Server
{
    public class WaterReplyDoer :Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public WaterReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "WaterReplyDoer";
        }
        
        public override void DoProtocol(Envelope message)
        {
            WaterReply incomingReply = message.Message as WaterReply;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyFightManager.FillBalloon(incomingReply.PlayerID, incomingReply.BalloonID, incomingReply.AmountOfWater);
                    break;
                case Reply.PossibleStatus.Invalid:
                    // TODO: ????
                    break;
            }
            
        }
        #endregion

        #region Private Methods
        protected override void Process()
        {
            while (keepGoing)
            {   
                Thread.Sleep(0);
            }
        }
         
        #endregion
    }
}
