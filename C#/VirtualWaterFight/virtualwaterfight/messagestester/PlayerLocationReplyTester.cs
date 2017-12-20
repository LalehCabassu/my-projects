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
    public class PlayerLocationReplyTester
    {
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
    }
}
