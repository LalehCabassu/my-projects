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
    public class RegistrationDoer : Doer
    {
        #region Data Members Getter/Setter
        
        private AckNak incomingAckNak;
        private WaterManager MyWaterManager;
        #endregion

        #region Public Methods
        
        public RegistrationDoer(Communicator communicator, WaterManager myWaterManager)
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
                    MyWaterManager.AddNewPlayer(incomingAckNak.ConversationId.ProcessId);
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");
            newReply.ConversationId = incomingAckNak.ConversationId;
            newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, MyWaterManager.FightManagerEP);    
        }
        #endregion

    }
}
