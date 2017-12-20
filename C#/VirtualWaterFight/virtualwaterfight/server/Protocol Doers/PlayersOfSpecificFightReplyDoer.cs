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
    public class PlayersOfSpecificFightReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public PlayersOfSpecificFightReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "PlayersOfSpecificFightReplyDoer";
        }

        public override void DoProtocol(Envelope message)
        {
            PlayersOfSpecificFightRequest incomingRequest = message.Message as PlayersOfSpecificFightRequest;
            IPEndPoint targetEP = message.SendersEP;
            WaterFightGame fight = MyFightManager.FindFight(incomingRequest.FightID);
            Int16[] list;
            
            if (fight != null)
            {
                list = new Int16[fight.PlayerList.Count];
                list = MyFightManager.ListPlayersOfspecificFight(fight);
                
                PlayersListReply newReply = new PlayersListReply(list, Reply.PossibleStatus.Valid, "Players Of Specific Fight");
                newReply.ConversationId = message.Message.ConversationId;
                newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
                Send(newReply, targetEP);
            }
        }
        #endregion
    }
}
