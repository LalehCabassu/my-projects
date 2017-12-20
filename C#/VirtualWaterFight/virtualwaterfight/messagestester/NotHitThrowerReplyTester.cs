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
    public class NotHitThrowerReplyTester
    {
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
    }
}
