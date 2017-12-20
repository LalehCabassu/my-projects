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
    public class InprocessFightsListReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public InprocessFightsListReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "InprocessFightsListReplyDoer";
        }

        public override void DoProtocol(Envelope message)
        {
            IPEndPoint targetEP = message.SendersEP;
            int[] list = MyFightManager.ListInprocessFights();

            InprocessFightsListReply newReply = new InprocessFightsListReply(list, Reply.PossibleStatus.Valid, "Inprocess Fights List");
            newReply.ConversationId = message.Message.ConversationId;
            newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, targetEP);
        }
        #endregion
    }
}
