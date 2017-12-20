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

namespace FightManager
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
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Balloon");
            newReply.ConversationId = incomingReply.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingReply.ConversationId.ProcessId, Convert.ToInt16(incomingReply.MessageNr.SeqNumber + 1));
            Send(newReply, MyFightManager.BalloonManagerEP);
        }
        #endregion
    }
}
