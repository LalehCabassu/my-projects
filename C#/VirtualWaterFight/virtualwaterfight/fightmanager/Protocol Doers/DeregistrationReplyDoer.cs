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
    public class DeregistrationReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
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
        
        public void DoProtocol(Envelope message, ManagerConversation conversation)
        {
            IPEndPoint targetEP = message.SendersEP;
            Player currentPlayer = MyFightManager.FindPlayer(targetEP);
            if (currentPlayer != null && currentPlayer.PlayerID == message.Message.ConversationId.ProcessId)
            {
                MyFightManager.RemovePlayer(targetEP);
                currentPlayer.removePlayerID();
                
                AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Deregistered");
                newReply.ConversationId = message.Message.ConversationId;
                newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));

                conversation.SendReply(newReply, newReply);
            }
        }
        #endregion
    }
}
