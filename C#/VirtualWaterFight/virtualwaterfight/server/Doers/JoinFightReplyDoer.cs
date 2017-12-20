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
        private enum possibleStates
        {
            JoinFightRequestReceived = 1,
            DecrementNumberOfBalloonsRequestSent = 2,
            HitReplySent = 3,
            NotHitReplySent = 4,
            HitThrowerReplySent = 5,
            NotHitThrowerReplySent = 6,
            DeregisterSent = 7
        }
        private IPEndPoint throwerEP;
        private IPEndPoint opponentEP;
        private JoinFightRequest incomingRequest;
        private Player opponent;
        private Player thrower;
        private WaterFightGame fight;
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

        public override void DoProtocol(Envelope message)
        {
            incomingRequest = message.Message as JoinFightRequest;
            throwerEP = message.SendersEP;
            opponentEP = MyFightManager.FindPlayerEP(incomingRequest.PlayerID);

            fight = MyFightManager.FindFight(incomingRequest.FightID);
            thrower = MyFightManager.FindPlayer(throwerEP);
            opponent = MyFightManager.FindPlayer(incomingRequest.PlayerID);
            Location opponentLocation = opponent.GetCurrentLocation();
            //bool hasBalloon = thrower.FindBalloon(incomingRequest.BalloonID, incomingRequest.AmountOfWater);
            //if (thrower != null && opponent != null && opponentLocation != null && hasBalloon)
            //if (thrower != null && opponent != null && opponentLocation != null && MyFightManager.isInFight(fight, thrower, opponent))
            if (thrower != null && opponent != null && opponentLocation != null)
            {
                if (incomingRequest.PlayerLocation.X == opponentLocation.X && incomingRequest.PlayerLocation.Y == opponentLocation.Y)
                {
                    MyFightManager.NewThrow(fight.FightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, true);

                    SendHitThrower();
                    SendDecrementBalloon();
                    if (opponent.HitByAmountOfWater > 100)
                        SendDeregister();
                    else
                        SendHit();
                }
                else
                {
                    MyFightManager.NewThrow(fight.FightID, thrower.PlayerID, opponent.PlayerID, incomingRequest.BalloonID, incomingRequest.AmountOfWater, false);
                    SendNotHitThrower("Not Hit");
                    SendNotHit("Not Hit");
                }
            }
            else
                SendNotHitThrower("Invalid information");
            
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

            //wfssWebAPI.GameStats[] myStats = service.GetGameStats(MyFightManager.FightManagerID.ToString(), 100);
            //System.Console.WriteLine("{0}:{1}, number of stats={2}", MyFightManager.FightManagerID, 100, myStats.Length);
        }

        #endregion

        #region Private Methods
        private void SendDeregister()
        {
            opponentEP = MyFightManager.FindPlayerEP(incomingRequest.PlayerID);
            if (opponentEP != null)
            {
                MyFightManager.RemovePlayer(opponentEP);
                AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Deregistered");

                //Set ConversationID and MessageID
                newReply.ConversationId = incomingRequest.ConversationId;
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, opponentEP);
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, MyFightManager.BalloonManagerEP);
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                base.Send((Message)newReply, MyFightManager.WaterManagerEP);
            }
        }

        private void SendHitThrower()
        {
            HitThrowerReply newReply = new HitThrowerReply(fight.FightID, opponent.PlayerID, Reply.PossibleStatus.Valid, "JoinFight");

            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, throwerEP);
        }

        private void SendNotHitThrower(string note)
        {
            NotHitThrowerReply newReply = new NotHitThrowerReply(opponent.PlayerID, Reply.PossibleStatus.Valid, "JoinFight: " + note);

            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, throwerEP);
        }

        private void SendHit()
        {
            HitReply newReply = new HitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                fight.FightID, incomingRequest.AmountOfWater, Reply.PossibleStatus.Valid, "JoinFight");

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = opponent.PlayerID;
            newReply.ConversationId = MessageNumber.Create();
            newReply.MessageNr = newReply.ConversationId;
            base.Send((Message)newReply, opponentEP);
        }

        private void SendNotHit(string note)
        {
            NotHitReply newReply = new NotHitReply(incomingRequest.ConversationId.ProcessId, MyFightManager.FindPlayer(opponentEP).GetCurrentLocation(),
                                                    Reply.PossibleStatus.Valid, "JoinFight: " + note);

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = opponent.PlayerID;
            newReply.ConversationId = MessageNumber.Create();
            base.Send((Message)newReply, opponentEP);
        }

        private void SendDecrementBalloon()
        {
            DecrementNumberOfBalloonsRequest newReq = new DecrementNumberOfBalloonsRequest(MyFightManager.FindPlayer(throwerEP).PlayerID, incomingRequest.BalloonID);

            //Set ConversationID and MessageID
            newReq.ConversationId = incomingRequest.ConversationId;
            newReq.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReq, MyFightManager.BalloonManagerEP);
            newReq.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReq, MyFightManager.WaterManagerEP);
        }
        
        protected override void Process()
        {
            while (keepGoing)
            {/*
                if (!suspended)
                {
                    
                }
              */
                Thread.Sleep(0);
            }
        }
        #endregion
    }
}
