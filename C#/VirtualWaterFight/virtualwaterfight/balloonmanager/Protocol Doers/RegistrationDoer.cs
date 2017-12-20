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

namespace BalloonManager
{
    public class RegistrationDoer : Doer
    {
        #region Data Members Getter/Setter
        
        private AckNak incomingAckNak;
        private BalloonManager MyBalloonManager;
        #endregion

        #region Public Methods
        
        public RegistrationDoer(Communicator communicator, BalloonManager myBalloonManager)
            : base(communicator)
        {
            MyBalloonManager = myBalloonManager;
        }

        public override void DoProtocol(Envelope message)
        {
            incomingAckNak = (AckNak)message.Message;

            switch (incomingAckNak.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyBalloonManager.AddNewPlayer(incomingAckNak.ConversationId.ProcessId);
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }

            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");
            newReply.ConversationId = incomingAckNak.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingAckNak.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, MyBalloonManager.FightManagerEP);
        }
        #endregion
    }
}
