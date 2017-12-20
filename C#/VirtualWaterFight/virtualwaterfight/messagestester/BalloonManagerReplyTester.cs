using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common;


namespace MessagesTester
{
    [TestClass]
    public class BalloonManagerReplyTester
    {
        [TestMethod]
        public void BalloonManagerReply_Test()
        {
            // Test Public Constructor
            BalloonManagerReply rep = new BalloonManagerReply(65, 3, Reply.PossibleStatus.Valid, "Numbur of empty balloons the player has");
            Assert.AreEqual(65, rep.PlayerID);
            Assert.AreEqual(3, rep.NumberOfBalloons);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("Numbur of empty balloons the player has", rep.Note);

            rep = new BalloonManagerReply(Int16.MaxValue, 4, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.PlayerID);
            Assert.AreEqual(4, rep.NumberOfBalloons);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new BalloonManagerReply(0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.PlayerID);
            Assert.AreEqual(0, rep.NumberOfBalloons);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            BalloonManagerReply rep_1 = new BalloonManagerReply(29, 2, Reply.PossibleStatus.Invalid, "Invalid Message");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            BalloonManagerReply rep_2 = BalloonManagerReply.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.PlayerID, rep_2.PlayerID);
            Assert.AreEqual(rep_1.NumberOfBalloons, rep_2.NumberOfBalloons);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
    }
}
