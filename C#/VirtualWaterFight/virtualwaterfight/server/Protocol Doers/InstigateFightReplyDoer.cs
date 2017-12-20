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
    public class InstigateFightReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        public string wfssResult;
        private FightManager MyFightManager;
        private IPEndPoint throwerEP;
        private IPEndPoint opponentEP;
        private InstigateFightRequest incomingRequest;
        private Player opponent;
        private Player thrower;
        private int newFightID;
        private ManagerConversation currentConversation;
        #endregion

        #region Public Methods
        public InstigateFightReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "InstigateFightReplyDoer";
        }

        public void DoProtocol(Envelope message, ManagerConversation conversation)
        {
            currentConversation = conversation;
            incomingRequest = message.Message as InstigateFightRequest;
            throwerEP = message.SendersEP;
            opponentEP = MyFightManager.FindPlayerEP(incomingRequest.PlayerID);

            thrower = MyFightManager.FindPlayer(throwerEP);
            opponent = MyFightManager.FindPlayer(incomingRequest.PlayerID);
            Location opponentLocation = opponent.GetCurrentLocation();
            if (thrower != null && opponent != null && opponentLocation != null)
            {
                if (incomingRequest.PlayerLocation.X == opponentLocation.X && incomingRequest.PlayerLocation.Y == opponentLocation.Y)
                {
                    newFightID = MyFightManager.AddFight();
                    MyFightManager.NewThrow(newFightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, true);

                    // WFSS
                    wfssWebAPI.WFStatsSoapClient service = new wfssWebAPI.WFStatsSoapClient();
                    wfssResult = service.LogNewGame(MyFightManager.FightManagerGUID.ToString(), newFightID, DateTime.Now);
                    
                    sendHitThrower(1);
                    sendDecrementBalloon(2);
                    if (opponent.HitByAmountOfWater > 100)
                        sendDeregister(3);
                    else
                        sendHit(3);
                }
                else
                {
                    MyFightManager.NewThrow(newFightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, false);
                    sendNotHitThrower("Not Hit", 1);
                    sendNotHit("Not Hit", 2);
                }
            }
            else
                sendNotHitThrower("Invalid information", 1);
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
            HitThrowerReply newReply = new HitThrowerReply(newFightID, opponent.PlayerID, Reply.PossibleStatus.Valid, "InstigateFight");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Reliable communication
            currentConversation.SendReplyToPlayer(newReply);
        }

        private void sendNotHitThrower(string note, int addToSeqNum)
        {
            NotHitThrowerReply newReply = new NotHitThrowerReply(opponent.PlayerID, Reply.PossibleStatus.Valid, "InstigateFight: " + note);
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Reliable communication
            currentConversation.SendReplyToPlayer(newReply);
        }

        private void sendHit(int addToSeqNum)
        {
            HitReply newReply = new HitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                newFightID, incomingRequest.AmountOfWater, Reply.PossibleStatus.Valid, "InstigateFight");
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            // Unreliable communication
            base.Send((Message)newReply, opponentEP);
        }

        private void sendNotHit(string note, int addToSeqNum)
        {
            NotHitReply newReply = new NotHitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                                                    Reply.PossibleStatus.Valid, "InstigateFight: " + note);
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
