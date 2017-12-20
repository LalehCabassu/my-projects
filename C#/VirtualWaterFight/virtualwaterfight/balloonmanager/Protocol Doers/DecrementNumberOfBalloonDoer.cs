using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Common;
using Common.Threading;
using Common.Messages;
using Common.Communicator;

namespace BalloonManager
{
    public class DecrementNumberOfBalloonDoer : Doer
    {
        #region Data Members Getter/Setter
        private BalloonManager MyBalloonManager;
        #endregion

        #region Public Methods
        
        public DecrementNumberOfBalloonDoer(Communicator communicator, BalloonManager myBalloonManager)
            : base(communicator)
        {
            MyBalloonManager = myBalloonManager;
        }

        public override void DoProtocol(Envelope message)
        {
            DecrementNumberOfBalloonsRequest incomingRequest = (DecrementNumberOfBalloonsRequest)message.Message;
            MyBalloonManager.RemoveBalloon(incomingRequest.PlayerID, incomingRequest.BalloonID);

            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "DecrementNumberOfBalloons");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, MyBalloonManager.FightManagerEP);
        }
        #endregion
    }
}
