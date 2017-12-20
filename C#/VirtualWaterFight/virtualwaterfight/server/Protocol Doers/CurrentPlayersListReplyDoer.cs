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
    public class CurrentPlayersListReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public CurrentPlayersListReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "CurrentPlayersListReplyDoer";
        }

        public override void DoProtocol(Envelope message)
        {
            IPEndPoint targetEP = message.SendersEP;
            Int16[] list = MyFightManager.ListCurrentPlayers();

            PlayersListReply newReply = new PlayersListReply(list, Reply.PossibleStatus.Valid, "Current Players");
            newReply.ConversationId = message.Message.ConversationId;
            newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            Send(newReply, targetEP);
        }
        #endregion

        
    }
}
