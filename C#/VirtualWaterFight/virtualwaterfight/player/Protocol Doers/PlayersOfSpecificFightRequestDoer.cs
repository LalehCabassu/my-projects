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

namespace Player
{
    public class PlayersOfSpecificFightRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private IPEndPoint targetEP;
        //private Int16 counter = 0;
        //private MessageNumber firstMessageNr;
        //private MessageNumber lastMessageNr;
        //private bool clearList = false;
        private int FightID;
        #endregion

        #region Public Methods
        public PlayersOfSpecificFightRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = myPlayer.FightManagerEP;
        }

        public override string ThreadName()
        {
            return "PlayersOfSpecificFightRequestDoer";
        }

        public void SendRequest(int fightID)
        {
            FightID = fightID; 
            PlayersOfSpecificFightRequest newRequest = new PlayersOfSpecificFightRequest(fightID);
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;
            Send((Message)newRequest, targetEP);
        }

        public override void DoProtocol(Envelope message)
        {
            PlayersListReply incomingReply = message.Message as PlayersListReply;
            MyPlayer.ClearPlayersOfSpecificFight(FightID);
            MyPlayer.UpdatePlayersOfSpecificFight(FightID, incomingReply.PlayersList);
        }
        #endregion
    }
}
