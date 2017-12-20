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
        private PlayerSettings settings;
        private enum possibleStates
        {
            RecentLocationsRequestSent = 1,
            LocationsListReceived = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private RecentLocationsReply incomingLocationsList;
        #endregion

        #region Public Methods
        public RecentLocationsRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "RecentLocationsRequestDoer";
        }

        public void SendRequest(Int16 playerID)
        {
            RecentLocationsRequest newRequest = new RecentLocationsRequest(playerID);

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);
            
            //conversationState.Add(possibleStates.RecentLocationsRequestSent, (Message)newRequest);
            
        }

        public override void DoProtocol(Envelope message)
        {
            incomingLocationsList = message.Message as RecentLocationsReply;
            switch (incomingLocationsList.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyPlayer.updateOpponentLocationList(incomingLocationsList.PlayerID, incomingLocationsList.LocationList);
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
                default:
                    break;
            }
        }

        #endregion

       
        /*
        private RecentLocationsReply RetrieveReply()
        {
            if (MessageAvailable())
                return (RecentLocationsReply)base.MyPlayer.RecentLocationsReplyQueue.Dequeue().Message;
            return null;
        }

        private void processLocationList()
        {
            if (!conversationState.ContainsValue(incomingLocationsList) && 
                conversationState[possibleStates.RecentLocationsRequestSent].ConversationId == incomingLocationsList.ConversationId)
            {
                if (conversationState[possibleStates.RecentLocationsRequestSent].ConversationId == incomingLocationsList.ConversationId)
                {
                    
                    //Update Conversation State
                    //if (!conversationState.ContainsKey(possibleStates.LocationsListReceived))
                        conversationState.Add(possibleStates.RecentLocationsRequestSent, (Message)incomingLocationsList);
                    //else
                      //  conversationState[possibleStates.RecentLocationsRequestSent] = (Message)incomingLocationsList;
                }
            }
        }
         */ 
        
    }
}
