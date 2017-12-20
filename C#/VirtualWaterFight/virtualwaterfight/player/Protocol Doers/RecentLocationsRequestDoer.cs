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
    public class RecentLocationsRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public RecentLocationsRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
        }

        public override string ThreadName()
        {
            return "RecentLocationsRequestDoer";
        }

        public void SendRequest(Int16 playerID)
        {
            RecentLocationsRequest newRequest = new RecentLocationsRequest(playerID);
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);
        }

        public override void DoProtocol(Envelope message)
        {
            RecentLocationsReply incomingLocationsList = message.Message as RecentLocationsReply;
            switch (incomingLocationsList.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyPlayer.UpdatePlayerLocation(incomingLocationsList.PlayerID, incomingLocationsList.LocationList);
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
        }
        #endregion
    }
}
