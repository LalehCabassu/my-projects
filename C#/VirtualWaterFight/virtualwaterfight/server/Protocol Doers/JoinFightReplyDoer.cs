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
    public class JoinFightReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        public string wfssResult;
        private FightManager MyFightManager;
        private IPEndPoint throwerEP;
        private IPEndPoint opponentEP;
        private JoinFightRequest incomingRequest;
        private Player opponent;
        private Player thrower;
        private WaterFightGame fight;
        private ManagerConversation currentConversation;
        #endregion

        #region Public Methods
        public JoinFightReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "JoinFightReplyDoer";
        }

        public void DoProtocol(Envelope message, ManagerConversation conversation)
        {
            currentConversation = conversation;
            incomingRequest = message.Message as JoinFightRequest;
            throwerEP = message.SendersEP;
            opponentEP = MyFightManager.FindPlayerEP(incomingRequest.PlayerID);

            fight = MyFightManager.FindFight(incomingRequest.FightID);
            thrower = MyFightManager.FindPlayer(throwerEP);
            opponent = MyFightManager.FindPlayer(incomingRequest.PlayerID);
            Location opponentLocation = opponent.GetCurrentLocation();
            if (thrower != null && opponent != null && opponentLocation != null)
            {
                if (incomingRequest.PlayerLocation.X == opponentLocation.X && incomingRequest.PlayerLocation.Y == opponentLocation.Y)
                {
                    MyFightManager.NewThrow(fight.FightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, true);

                    sendHitThrower(1);
                    sendDecrementBalloon(2);
                    if (opponent.HitByAmountOfWater > 100)
                        sendDeregister(3);
                    else
                        sendHit(3);
                }
                else
                {
                    MyFightManager.NewThrow(fight.FightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, false);
                    sendNotHitThrower("Not Hit", 1);
                    sendNotHit("Not Hit", 2);
                }
            }
            else
                sendNotHitThrower("Invalid information", 1);

            //WFSS
            wfssWebAPI.WFStatsSoapClient service = new wfssWebAPI.WFStatsSoapClient();
            wfssWebAPI.GameStats stats = new wfssWebAPI.GameStats();
            stats.GameStatus = wfssWebAPI.PossibleGameStates.InProgress;
            stats.CurrentNumberOfPlayers = fight.NumberOfPlayers();
            stats.LargestNumberOfPlayers = fight.LargestNumberOfPlayers;
            stats.NumberOfBalloonsThrown = fight.TotalNumberOfBalloonsThrown;
            stats.NumberOfBalloonsThatHit = fight.TotalNumberOfBalloonHit;
            stats.AmountOfWaterThrown = fight.TotalAmountOfWaterThrown;
            stats.AmountOfWaterThatHit = fight.TotalAmountOfWaterHit;
            stats.Timestamp = DateTime.Now;
            wfssResult = service.LogGameStats(MyFightManager.FightManagerGUID.ToString(), fight.FightID, stats);
        }

        #endregion

        #region Private Methods
        private void sendDeregister(int addToSeqNum)
        {
            opponentEP = MyFightManager.FindPlayerEP(incomingRequest.PlayerID);
            if (opponentEP != null)
            {
                MyFightManager.RemovePlayer(opponentEP);
                AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Deregistered");
                newReply.ConversationId = incomingRequest.ConversationId;
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

                // Reliable communication
                currentConversation.SendReplyToPlayer(newReply);
                currentConversation.SendReplyToManagers(newReply);
            }
        }

        private void sendHitThrower(int addToSeqNum)
        {
            HitThrowerReply newReply = new HitThrowerReply(fight.FightID, opponent.PlayerID, Reply.PossibleStatus.Valid, "JoinFight");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Reliable communication
            currentConversation.SendReplyToPlayer(newReply);
        }

        private void sendNotHitThrower(string note, int addToSeqNum)
        {
            NotHitThrowerReply newReply = new NotHitThrowerReply(opponent.PlayerID, Reply.PossibleStatus.Valid, "JoinFight: " + note);
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Reliable communication
            currentConversation.SendReplyToPlayer(newReply);
        }

        private void sendHit(int addToSeqNum)
        {
            HitReply newReply = new HitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                fight.FightID, incomingRequest.AmountOfWater, Reply.PossibleStatus.Valid, "JoinFight");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Unreliable communication
            base.Send((Message)newReply, opponentEP);
        }

        private void sendNotHit(string note, int addToSeqNum)
        {
            NotHitReply newReply = new NotHitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                                                    Reply.PossibleStatus.Valid, "JoinFight: " + note);
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Unreliable communication
            base.Send((Message)newReply, opponentEP);
        }

        private void sendDecrementBalloon(int addToSeqNum)
        {
            DecrementNumberOfBalloonsRequest newReq = new DecrementNumberOfBalloonsRequest(MyFightManager.FindPlayer(throwerEP).PlayerID, incomingRequest.BalloonID);
            newReq.ConversationId = incomingRequest.ConversationId;
            newReq.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Reliable communication
            currentConversation.SendReplyToManagers(newReq);
        }
        #endregion
    }
}
