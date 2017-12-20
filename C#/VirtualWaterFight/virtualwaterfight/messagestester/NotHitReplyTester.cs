using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common;
using Common.Messages;

namespace MessagesTester
{
    [TestClass]
    public class NotHitReplyTester
    {
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
    }
}
