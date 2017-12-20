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
    public class InprocessFightsListRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public InprocessFightsListRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = myPlayer.FightManagerEP;
        }

        public override string ThreadName()
        {
            return "InprogressFightsListRequestDoer";
        }

        public void SendRequest()
        {
            InprocessFightsListRequest newRequest = new InprocessFightsListRequest();
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;
            Send((Message)newRequest, targetEP);
        }

        public override void DoProtocol(Envelope message)
        {
            InprocessFightsListReply incomingPacketizedList = message.Message as InprocessFightsListReply;
            MyPlayer.ClearInprocessFightsList();
            MyPlayer.UpdateInprocessFightsList(incomingPacketizedList.FightsList);
        }
        #endregion
    }
}
