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
    public class InstigateFightRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private PlayerConversationList myConversationList;
        private Location opponentLocation;
        #endregion

        #region Public Methods
        public InstigateFightRequestDoer(Communicator communicator, Player myPlayer, PlayerConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            myConversationList = conversationList;
        }

        public override string ThreadName()
        {
            return "InstigateFightRequestDoer";
        }

        public void SendRequest(Int16 playerID, Location playerLocation, Int16 amountOfWater, int balloonID)
        {
            opponentLocation = playerLocation;
            InstigateFightRequest newRequest = new InstigateFightRequest(playerID, playerLocation, amountOfWater, balloonID);
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId = MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            PlayerConversation currentConversation = myConversationList.AddNewConversation(newRequest, MyPlayer.FightManagerEP);
            currentConversation.SendRequest();

            MyPlayer.DecrementNumberOfBalloons(balloonID);
        }

        public void DoHit(Envelope message)
        {
            HitReply incomingHitReply = message.Message as HitReply;
            MyPlayer.NewFight(incomingHitReply.FightID, incomingHitReply.ThrowerID, incomingHitReply.ThrowerLocation, incomingHitReply.AmountOfWater);
        }

        public void DoNotHit(Envelope message)
        {
            NotHitReply incomingNotHitReply = message.Message as NotHitReply;
            MyPlayer.NewOpponent(incomingNotHitReply.ThrowerID, incomingNotHitReply.ThrowerLocation);
        }

        public void DoHitThrower(Envelope message)
        {
            HitThrowerReply incomingHitThrowerReply = message.Message as HitThrowerReply;
            MyPlayer.NewFight(incomingHitThrowerReply.FightID, incomingHitThrowerReply.OpponentID, opponentLocation, 0);
        }
        #endregion
    }
}
