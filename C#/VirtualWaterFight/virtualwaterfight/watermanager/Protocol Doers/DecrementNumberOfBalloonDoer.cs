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
            DecrementNumberOfBalloonsRequest incomingRequest = (DecrementNumberOfBalloonsRequest)message.Message;
            MyWaterManager.RemoveBalloon(incomingRequest.PlayerID, incomingRequest.BalloonID);

            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "DecrementNumberOfBalloons");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, MyWaterManager.FightManagerEP);
        }
        #endregion
    }
}
