using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common;
using Common.Messages;

namespace TestVirtualWaterFight
{
    [TestClass]
    public class MessageTester
    {
        /*
        [TestMethod]
        public void AckNak_Test()
        {
            // Test Public Constructor
            AckNak rep = new AckNak(Reply.PossibleStatus.Valid, "Successful Deregistration");
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("Successful Deregistration", rep.Note);

            rep = new AckNak(Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new AckNak(Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            AckNak rep_1 = new AckNak(Reply.PossibleStatus.Invalid, "Failed to hit the player.");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            AckNak rep_2 = AckNak.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }

        [TestMethod]
        public void CurrentPlayersListRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            CurrentPlayersListRequest req_1 = new CurrentPlayersListRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            CurrentPlayersListRequest req_2 = CurrentPlayersListRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }

        [TestMethod]
        public void DecrementNumberOfBalloonsRequest_Test()
        {
            // Test Public Constructor
            DecrementNumberOfBalloonsRequest req = new DecrementNumberOfBalloonsRequest(req.PlayerID, 127);
            Assert.AreEqual(127, req.PlayerID);

            req = new DecrementNumberOfBalloonsRequest(req.PlayerID, Int16.MaxValue);
            Assert.AreEqual(Int16.MaxValue, req.PlayerID);

            req = new DecrementNumberOfBalloonsRequest(req.PlayerID, 0);
            Assert.AreEqual(0, req.PlayerID);

            // Test Create Factory Method
            DecrementNumberOfBalloonsRequest req_1 = new DecrementNumberOfBalloonsRequest(req.PlayerID, 21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            DecrementNumberOfBalloonsRequest req_2 = DecrementNumberOfBalloonsRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerID, req_2.PlayerID);
        }

        [TestMethod]
        public void DeregistrationRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            DeregistrationRequest req_1 = new DeregistrationRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            DeregistrationRequest req_2 = DeregistrationRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }

        [TestMethod]
        public void EmptyBalloonRequest_Test()
        {
            // Test Public Constructor
            EmptyBalloonRequest req = new EmptyBalloonRequest(EmptyBalloonRequest.PossibleSize.Medium, EmptyBalloonRequest.PossibleColor.Brown);
            Assert.AreEqual(EmptyBalloonRequest.PossibleSize.Medium, req.Size);
            Assert.AreEqual(EmptyBalloonRequest.PossibleColor.Brown, req.Color);
            
            // Test Create Factory Method
            EmptyBalloonRequest req_1 = new EmptyBalloonRequest(EmptyBalloonRequest.PossibleSize.Large, EmptyBalloonRequest.PossibleColor.Green);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            EmptyBalloonRequest req_2 = EmptyBalloonRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.Size, req_2.Size);
            Assert.AreEqual(req_1.Color, req_2.Color);
        }
         * 
        [TestMethod]
        public void FightManagerReply_Test()
        {
            // Test Public Constructor
            FightManagerReply rep = new FightManagerReply(65, 423214, Reply.PossibleStatus.Valid, "Number of Fights the player participates");
            Assert.AreEqual(65, rep.PlayerID);
            Assert.AreEqual(423214, rep.NumberOfFights);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("Number of Fights the player participates", rep.Note);

            rep = new FightManagerReply(Int16.MaxValue, Int32.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.PlayerID);
            Assert.AreEqual(Int32.MaxValue, rep.NumberOfFights);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new FightManagerReply(0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.PlayerID);
            Assert.AreEqual(0, rep.NumberOfFights);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            FightManagerReply rep_1 = new FightManagerReply(29, 846579, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            FightManagerReply rep_2 = FightManagerReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.PlayerID, rep_2.PlayerID);
            Assert.AreEqual(rep_1.NumberOfFights, rep_2.NumberOfFights);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void HitReply_Test()
        {
            // Test Public Constructor
            Location loc = new Location(9866, 423);
            HitReply rep = new HitReply(76, loc, 43, 5, Reply.PossibleStatus.Valid, "InstigateFight");
            Assert.AreEqual(76, rep.ThrowerID);
            Assert.AreEqual(loc.X, rep.ThrowerLocation.X);
            Assert.AreEqual(loc.Y, rep.ThrowerLocation.Y);
            Assert.AreEqual(43, rep.FightID);
            Assert.AreEqual(5, rep.AmountOfWater);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("InstigateFight", rep.Note);
            
            // Test Create Factory Method
            Location loc1 = new Location(1, 2);
            HitReply rep_1 = new HitReply(12, loc1, 10, 543, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            Reply rep_2 = (Reply)Message.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);
            
        }
         * 
        [TestMethod]
        public void HitThrowerReply_Test()
        {
            // Test Public Constructor
            HitThrowerReply rep = new HitThrowerReply(65744, 14, Reply.PossibleStatus.Valid, "Yes, you hit the opponent.");
            Assert.AreEqual(65744, rep.FightID);
            Assert.AreEqual(14, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("Yes, you hit the opponent.", rep.Note);

            rep = new HitThrowerReply(Int32.MaxValue, Int16.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int32.MaxValue, rep.FightID);
            Assert.AreEqual(Int16.MaxValue, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new HitThrowerReply(0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.FightID);
            Assert.AreEqual(0, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            HitThrowerReply rep_1 = new HitThrowerReply(298465, 79, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            HitThrowerReply rep_2 = HitThrowerReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.FightID, rep_2.FightID);
            Assert.AreEqual(rep_1.OpponentID, rep_2.OpponentID);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void InprocessFightsListRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            InprocessFightsListRequest req_1 = new InprocessFightsListRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            InprocessFightsListRequest req_2 = InprocessFightsListRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }
         * 
        [TestMethod]
        public void InstigateFightRequest_Test()
        {
            // Test Public Constructor
            InstigateFightRequest req = new InstigateFightRequest(810, 127);
            Assert.AreEqual(810, req.PlayerLocation);
            Assert.AreEqual(127, req.AmountOfWater);

            req = new InstigateFightRequest(int.MaxValue, Int16.MaxValue);
            Assert.AreEqual(int.MaxValue, req.PlayerLocation);
            Assert.AreEqual(Int16.MaxValue, req.AmountOfWater);

            req = new InstigateFightRequest(0, 0);
            Assert.AreEqual(0, req.PlayerLocation);
            Assert.AreEqual(0, req.AmountOfWater);

            // Test Create Factory Method
            InstigateFightRequest req_1 = new InstigateFightRequest(201, 35);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            InstigateFightRequest req_2 = InstigateFightRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerLocation, req_2.PlayerLocation);
            Assert.AreEqual(req_1.AmountOfWater, req_2.AmountOfWater);
        }
         * 
        [TestMethod]
        public void JoinFightRequest_Tester()
        {
            // Test Public Constructor
            JoinFightRequest req = new JoinFightRequest(810,127, 50);
            Assert.AreEqual(810, req.FightID);
            Assert.AreEqual(127, req.PlayerLocation);
            Assert.AreEqual(50, req.AmountOfWater);


            req = new JoinFightRequest(int.MaxValue, int.MaxValue, Int16.MaxValue);
            Assert.AreEqual(int.MaxValue, req.FightID);
            Assert.AreEqual(int.MaxValue, req.PlayerLocation);
            Assert.AreEqual(Int16.MaxValue, req.AmountOfWater);

            req = new JoinFightRequest(0, 0, 0);
            Assert.AreEqual(0, req.FightID);
            Assert.AreEqual(0, req.PlayerLocation);
            Assert.AreEqual(0, req.AmountOfWater);

            // Test Create Factory Method
            JoinFightRequest req_1 = new JoinFightRequest(201, 35, 25);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            JoinFightRequest req_2 = JoinFightRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.FightID, req_2.FightID);
            Assert.AreEqual(req_1.PlayerLocation, req_2.PlayerLocation);
            Assert.AreEqual(req_1.AmountOfWater, req_2.AmountOfWater);
        }
         * 
        [TestMethod]
        public void NotHitReply_Test()
        {
            // Test Public Constructor
            NotHitReply rep = new NotHitReply(59, 54423, Reply.PossibleStatus.Valid, "You're NOT hit by an opponent.");
            Assert.AreEqual(59, rep.ThrowerID);
            Assert.AreEqual(54423, rep.ThrowerLocation);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("You're NOT hit by an opponent.", rep.Note);

            rep = new NotHitReply(Int16.MaxValue, Int32.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.ThrowerID);
            Assert.AreEqual(Int32.MaxValue, rep.ThrowerLocation);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new NotHitReply(0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.ThrowerID);
            Assert.AreEqual(0, rep.ThrowerLocation);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            NotHitReply rep_1 = new NotHitReply(35, 549302, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            NotHitReply rep_2 = NotHitReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.ThrowerID, rep_2.ThrowerID);
            Assert.AreEqual(rep_1.ThrowerLocation, rep_2.ThrowerLocation);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void NotHitThrowerReply_Test()
        {
            // Test Public Constructor
            NotHitThrowerReply rep = new NotHitThrowerReply(34, Reply.PossibleStatus.Valid, "Not hit the opponent.");
            Assert.AreEqual(34, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("Not hit the opponent.", rep.Note);

            rep = new NotHitThrowerReply(Int16.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new NotHitThrowerReply(0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.OpponentID);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            NotHitThrowerReply rep_1 = new NotHitThrowerReply(27, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            NotHitThrowerReply rep_2 = NotHitThrowerReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.OpponentID, rep_2.OpponentID);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void NumberOfEmptyBalloonsRequest_Test()
        {
            // Test Public Constructor
            NumberOfEmptyBalloonsRequest req = new NumberOfEmptyBalloonsRequest(563);
            Assert.AreEqual(563, req.PlayerID);

            req = new NumberOfEmptyBalloonsRequest(Int16.MaxValue);
            Assert.AreEqual(Int16.MaxValue, req.PlayerID);

            req = new NumberOfEmptyBalloonsRequest(0);
            Assert.AreEqual(0, req.PlayerID);

            // Test Create Factory Method
            NumberOfEmptyBalloonsRequest req_1 = new NumberOfEmptyBalloonsRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            NumberOfEmptyBalloonsRequest req_2 = NumberOfEmptyBalloonsRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerID, req_2.PlayerID);
        }
         * 
        [TestMethod]
        public void NumberOfFightsRequest_Test()
        {
            // Test Public Constructor
            NumberOfFightsRequest req = new NumberOfFightsRequest(283);
            Assert.AreEqual(283, req.PlayerID);

            req = new NumberOfFightsRequest(Int16.MaxValue);
            Assert.AreEqual(Int16.MaxValue, req.PlayerID);

            req = new NumberOfFightsRequest(0);
            Assert.AreEqual(0, req.PlayerID);

            // Test Create Factory Method
            NumberOfFightsRequest req_1 = new NumberOfFightsRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            NumberOfFightsRequest req_2 = NumberOfFightsRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerID, req_2.PlayerID);
        }
         * 
        [TestMethod]
        public void PacketizedFightsListReply_Test()
        {
            // Test Public Constructor
            Random randInt = new Random();
            int[] fightList = new int[10];

            for (int i = 0; i < 10; i++)
                fightList[i] = randInt.Next();

            PacketizedFightsListReply rep = new PacketizedFightsListReply(fightList, Reply.PossibleStatus.Valid, "The first subList");
            Assert.AreEqual(fightList, rep.FightsSubList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("The first subList", rep.Note);

            for (int i = 0; i < 10; i++)
                fightList[i] = randInt.Next(Int16.MaxValue, Int32.MaxValue);
            rep = new PacketizedFightsListReply(fightList, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(fightList, rep.FightsSubList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            for (int i = 0; i < 10; i++)
                fightList[i] = 0;
            rep = new PacketizedFightsListReply(fightList, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(fightList, rep.FightsSubList);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            for (int i = 0; i < 10; i++)
                fightList[i] = randInt.Next();
            PacketizedFightsListReply rep_1 = new PacketizedFightsListReply(fightList, Reply.PossibleStatus.Invalid, "The first subList");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            PacketizedFightsListReply rep_2 = PacketizedFightsListReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            for(int i = 0; i < 10; i++)
                Assert.AreEqual(rep_1.FightsSubList[i], rep_2.FightsSubList[i]);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void PacketizedPlayersListReply_Test()
        {
            // Test Public Constructor
            Random randInt = new Random();
            int[,] playerList = new int[10, 2];

            for (int i = 0; i < 10; i++)
            {
                playerList[i, 0] = randInt.Next(0, Int16.MaxValue);
                playerList[i, 1] = randInt.Next();
            }

            PacketizedPlayersListReply rep = new PacketizedPlayersListReply(playerList, Reply.PossibleStatus.Valid, "The last subList");
            Assert.AreEqual(playerList, rep.PlayersSubList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("The last subList", rep.Note);

            for (int i = 0; i < 10; i++)
            {
                playerList[i, 0] = randInt.Next(0, Int16.MaxValue);
                playerList[i, 1] = randInt.Next(Int16.MaxValue, int.MaxValue);
            }
            rep = new PacketizedPlayersListReply(playerList, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(playerList, rep.PlayersSubList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            for (int i = 0; i < 10; i++)
            {
                playerList[i, 0] = 0;
                playerList[i, 1] = 0;
            }
            rep = new PacketizedPlayersListReply(playerList, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(playerList, rep.PlayersSubList);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            for (int i = 0; i < 10; i++)
            {
                playerList[i, 0] = randInt.Next(0, Int16.MaxValue);
                playerList[i, 1] = randInt.Next();
            }
            PacketizedPlayersListReply rep_1 = new PacketizedPlayersListReply(playerList, Reply.PossibleStatus.Invalid, "The first subList");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            PacketizedPlayersListReply rep_2 = PacketizedPlayersListReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(rep_1.PlayersSubList[i, 0], rep_2.PlayersSubList[i, 0]);
                Assert.AreEqual(rep_1.PlayersSubList[i, 1], rep_2.PlayersSubList[i, 1]);
            }
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
         [TestMethod]
        public void PlayerLocationReply_Test()
        {
            // Test Public Constructor
            PlayerLocationReply rep = new PlayerLocationReply(908, Reply.PossibleStatus.Valid, "The last location");
            Assert.AreEqual(908, rep.Location);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("The last location", rep.Note);

            rep = new PlayerLocationReply(int.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(int.MaxValue, rep.Location);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new PlayerLocationReply(0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.Location);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            PlayerLocationReply rep_1 = new PlayerLocationReply(108676, Reply.PossibleStatus.Invalid, "Failed to hit the player.");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            PlayerLocationReply rep_2 = PlayerLocationReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.Location, rep_2.Location);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void PlayerLocationRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            PlayerLocationRequest req_1 = new PlayerLocationRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            PlayerLocationRequest req_2 = PlayerLocationRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }
         * 
        [TestMethod]
        public void RecentLocationsReply_Test()
        {
            // Test Public Constructor
            Random randInt = new Random();
            int[] locationList = new int[10];

            for (int i = 0; i < 10; i++)
                locationList[i] = randInt.Next();

            RecentLocationsReply rep = new RecentLocationsReply(locationList, Reply.PossibleStatus.Valid, "The first subList");
            Assert.AreEqual(locationList, rep.LocationList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("The first subList", rep.Note);

            for (int i = 0; i < 10; i++)
                locationList[i] = randInt.Next(Int16.MaxValue, Int32.MaxValue);
            rep = new RecentLocationsReply(locationList, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(locationList, rep.LocationList);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            for (int i = 0; i < 10; i++)
                locationList[i] = 0;
            rep = new RecentLocationsReply(locationList, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(locationList, rep.LocationList);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            for (int i = 0; i < 10; i++)
                locationList[i] = randInt.Next();
            RecentLocationsReply rep_1 = new RecentLocationsReply(locationList, Reply.PossibleStatus.Invalid, "The first subList");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            RecentLocationsReply rep_2 = RecentLocationsReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(rep_1.LocationList[i], rep_2.LocationList[i]);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
         * 
        [TestMethod]
        public void RecentLocationsRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            RecentLocationsRequest req_1 = new RecentLocationsRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            RecentLocationsRequest req_2 = RecentLocationsRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }
         * 
         [TestMethod]
        public void RegistrationRequest_Test()
        {
            // Test Public Constructor
            RegistrationRequest req = new RegistrationRequest("Joe", 17, true, 100);
            Assert.AreEqual("Joe", req.Name);
            Assert.AreEqual(17, req.Age);
            Assert.AreEqual(true, req.Gender);
            Assert.AreEqual(100, req.Location);

            req = new RegistrationRequest("longUsername-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()",
                                                            Int16.MaxValue, true, Int32.MaxValue);
            Assert.AreEqual("longUsername-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", req.Name);
            Assert.AreEqual(Int16.MaxValue, req.Age);
            Assert.AreEqual(true, req.Gender);
            Assert.AreEqual(Int32.MaxValue, req.Location);
            
            req = new RegistrationRequest("", 0, false, 0);
            Assert.AreEqual("", req.Name);
            Assert.AreEqual(0, req.Age);
            Assert.AreEqual(false, req.Gender);
            Assert.AreEqual(0, req.Location);

            req = new RegistrationRequest(null, 0, false, 0);
            Assert.AreEqual(null, req.Name);
            Assert.AreEqual(0, req.Age);
            Assert.AreEqual(false, req.Gender);
            Assert.AreEqual(0, req.Location);

            // Test Create Factory Method
            RegistrationRequest req_1 = new RegistrationRequest("Henry", 21, true, 205);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            RegistrationRequest req_2 = RegistrationRequest.Create(bytes);
            Assert.IsNotNull(req_2);
            
            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.Name, req_2.Name);
            Assert.AreEqual(req_1.Age, req_2.Age);
            Assert.AreEqual(req_1.Gender, req_2.Gender);
            Assert.AreEqual(req_1.Location, req_2.Location);
            
        }
         * 
        [TestMethod]
        public void SpecificFightPlayersListRequest_Test()
        {
            // Test Public Constructor
            SpecificFightPlayersListRequest req = new SpecificFightPlayersListRequest(809);
            Assert.AreEqual(809, req.FightID);

            req = new SpecificFightPlayersListRequest(Int32.MaxValue);
            Assert.AreEqual(Int32.MaxValue, req.FightID);

            req = new SpecificFightPlayersListRequest(0);
            Assert.AreEqual(0, req.FightID);

            // Test Create Factory Method
            SpecificFightPlayersListRequest req_1 = new SpecificFightPlayersListRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            SpecificFightPlayersListRequest req_2 = SpecificFightPlayersListRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.FightID, req_2.FightID);
        }
         * 
        [TestMethod]
        public void WaterRequest_Test()
        {
            // Test Public Constructor
            WaterRequest req = new WaterRequest(57, WaterRequest.PossibleSize.Medium);
            Assert.AreEqual(57, req.PercentFilled);
            Assert.AreEqual(WaterRequest.PossibleSize.Medium, req.BalloonSize);

            // Test Create Factory Method
            WaterRequest req_1 = new WaterRequest(87, WaterRequest.PossibleSize.XLarge);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            WaterRequest req_2 = WaterRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PercentFilled, req_2.PercentFilled);
            Assert.AreEqual(req_1.BalloonSize, req_2.BalloonSize);
        }
         */
        
    }
}
