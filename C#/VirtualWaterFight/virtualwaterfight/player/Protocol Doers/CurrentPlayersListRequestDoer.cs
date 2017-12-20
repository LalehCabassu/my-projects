using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common.Communicator;
using Common.Messages;
using Common.Threading;

namespace Player
{
    public class CurrentPlayersListRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private IPEndPoint targetEP;
        private Player MyPlayer;
        #endregion

        #region Public Methods
        public CurrentPlayersListRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = myPlayer.FightManagerEP;
        }

        public override string ThreadName()
        {
            return "CurrentPlayerListRequestDoer";
        }

        public void SendRequest()
        {
            CurrentPlayersListRequest newRequest = new CurrentPlayersListRequest();
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;
            Send((Message)newRequest, targetEP);    
        }

        public override void DoProtocol(Envelope message)
        {
            PlayersListReply incomingPacketizedList = message.Message as PlayersListReply;
            MyPlayer.UpdateCurrentPlayersList(incomingPacketizedList.PlayersList);
        }
        #endregion
    }
}
