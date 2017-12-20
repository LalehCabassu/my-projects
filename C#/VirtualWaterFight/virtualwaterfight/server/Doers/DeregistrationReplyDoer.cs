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
    public class DeregistrationReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public DeregistrationReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "DeregistrationReplyDoer";
        }
        
        public override void DoProtocol(Envelope message)
        {
            targetEP = message.SendersEP;
            Player currentPlayer = MyFightManager.FindPlayer(targetEP);
            if (currentPlayer != null && currentPlayer.PlayerID == message.Message.ConversationId.ProcessId)
            {
                MyFightManager.RemovePlayer(targetEP);
                currentPlayer.removePlayerID();
                AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Deregistered");
                
                //Set ConversationID and MessageID
                newReply.ConversationId = message.Message.ConversationId;
                newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, targetEP);
                newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, MyFightManager.BalloonManagerEP);
                newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, MyFightManager.WaterManagerEP);
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
