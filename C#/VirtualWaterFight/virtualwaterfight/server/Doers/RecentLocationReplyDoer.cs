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
    public class RecentLocationReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        private enum possibleStates
        {
            RecentLocationRequestReceived = 1,
            LocationListSent = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private static int SubListDimension_1 = 10;
        private static int SubListDimension_2 = 2;
        #endregion

        #region Public Methods
        public RecentLocationReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "RecentLocationReplyDoer";
        }

        public override void  DoProtocol(Envelope message)
        {
 	        RecentLocationsRequest incomingRequest = message.Message as RecentLocationsRequest;
            targetEP = message.SendersEP;

            int[,] locationList = MyFightManager.PlayerRecentLocations(incomingRequest.PlayerID, SubListDimension_1);
            RecentLocationsReply newReply;
            if(locationList != null)
                newReply = new RecentLocationsReply(incomingRequest.PlayerID, locationList, Reply.PossibleStatus.Valid, "Recent Player Locations");
            else
                newReply = new RecentLocationsReply(incomingRequest.PlayerID, locationList, Reply.PossibleStatus.Invalid, "Recent Player Locations");
            
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, targetEP);
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
