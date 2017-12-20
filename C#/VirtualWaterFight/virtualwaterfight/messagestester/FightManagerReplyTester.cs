﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common;
using Common.Messages;

namespace MessagesTester
{
    [TestClass]
    public class FightManagerReplyTester
    {
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
    }
}
