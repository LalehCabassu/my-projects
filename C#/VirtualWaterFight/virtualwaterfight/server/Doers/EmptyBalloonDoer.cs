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
    public class EmptyBalloonDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public EmptyBalloonDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "EmptyBalloonDoer";
        }
        
        public override void DoProtocol(Envelope message)
        {
            EmptyBalloonReply incomingReply = message.Message as EmptyBalloonReply;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyFightManager.AddBalloon(incomingReply.PlayerID, incomingReply.BalloonID);
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
