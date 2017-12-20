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
    public class JointFightRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private enum possibleStates
        {
            JoinFightRequestSent = 1,
            HitThrowerReplyReceived = 2,
            NotHitThrowerReplyReceived = 3,
            HitReplyReceived = 4,
            NotHitReplyReceived = 5
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private Location opponentLocation;
        #endregion

        #region Public Methods
        public JointFightRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "JointFightRequestDoer";
        }

        public void SendRequest(int fightID, Int16 playerID, Location playerLocation, Int16 amountOfWater, int balloonID)
        {
            JoinFightRequest newRequest = new JoinFightRequest(fightID, playerID, playerLocation, amountOfWater, balloonID);

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);

            opponentLocation = playerLocation;

            //Update Conversation State
            if (conversationState.ContainsKey(possibleStates.JoinFightRequestSent))
                conversationState[possibleStates.JoinFightRequestSent] = (Message)newRequest;
            else
                conversationState.Add(possibleStates.JoinFightRequestSent, (Message)newRequest);
        }
        public void DoHit(Envelope message)
        {
            HitReply incomingHitReply = message.Message as HitReply;
            MyPlayer.NewFight(incomingHitReply.FightID, incomingHitReply.ThrowerID, incomingHitReply.ThrowerLocation, incomingHitReply.AmountOfWater);

            //Update Conversation State
            conversationState.Add(possibleStates.HitReplyReceived, message.Message);
        }

        public void DoNotHit(Envelope message)
        {
            NotHitReply incomingNotHitReply = message.Message as NotHitReply;
            MyPlayer.NewOpponent(incomingNotHitReply.ThrowerID, incomingNotHitReply.ThrowerLocation);

            //Update Conversation State
            conversationState.Add(possibleStates.NotHitReplyReceived, message.Message);
        }

        public void DoHitThrower(Envelope message)
        {
            HitThrowerReply incomingHitThrowerReply = message.Message as HitThrowerReply;

            if (!conversationState.ContainsValue(incomingHitThrowerReply) &&
                conversationState[possibleStates.JoinFightRequestSent].ConversationId == incomingHitThrowerReply.ConversationId)
            {
                MyPlayer.NewFight(incomingHitThrowerReply.FightID, incomingHitThrowerReply.OpponentID, opponentLocation, 0);

                //Update Conversation State
                conversationState.Add(possibleStates.HitThrowerReplyReceived, message.Message);
            }
        }

        public void DoNotHitThrower(Envelope message)
        {
            NotHitThrowerReply incomingNotHitThrowerReply = message.Message as NotHitThrowerReply;
            if (!conversationState.ContainsValue(incomingNotHitThrowerReply) &&
                conversationState[possibleStates.JoinFightRequestSent].ConversationId == incomingNotHitThrowerReply.ConversationId)
                //Update Conversation State
                conversationState.Add(possibleStates.HitThrowerReplyReceived, message.Message);
        }
        #endregion

       
    }
}
