using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common;
using Common.Messages;
using Objects;

namespace MessagesTester
{
    [TestClass]
    public class HitReplyTester
    {
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
            /*
            rep = new HitReply(Int16.MaxValue, Int32.MaxValue, Int32.MaxValue, Int16.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.ThrowerID);
            Assert.AreEqual(Int32.MaxValue, rep.ThrowerLocation);
            Assert.AreEqual(Int32.MaxValue, rep.FightID);
            Assert.AreEqual(Int16.MaxValue, rep.AmountOfWater);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new HitReply(0, 0, 0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.ThrowerID);
            Assert.AreEqual(0, rep.ThrowerLocation);
            Assert.AreEqual(0, rep.FightID);
            Assert.AreEqual(0, rep.AmountOfWater);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);
            */
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
            /*
            Assert.AreEqual(rep_1.ThrowerID, rep_2.ThrowerID);
            Assert.AreEqual(rep_1.ThrowerLocation.X, rep_2.ThrowerLocation.X);
            Assert.AreEqual(rep_1.ThrowerLocation.Y, rep_2.ThrowerLocation.Y);
            Assert.AreEqual(rep_1.FightID, rep_2.FightID);
            Assert.AreEqual(rep_1.AmountOfWater, rep_2.AmountOfWater);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
             */ 
        }
    }
}
