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
    public class AckNakListTester
    {
        [TestMethod]
        public void Ack_Test()
        {
            // Test Public Constructor
            AckNakList rep = new AckNakList(365, 908, Reply.PossibleStatus.Valid, "The 4th to 6th sublists are OK");
            Assert.AreEqual(365, rep.FirstMessageNr);
            Assert.AreEqual(908, rep.LastMessageNr);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("The 4th to 6th sublists are OK", rep.Note);

            rep = new AckNakList(Int16.MaxValue, Int16.MaxValue, Reply.PossibleStatus.Valid, "longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()");
            Assert.AreEqual(Int16.MaxValue, rep.FirstMessageNr);
            Assert.AreEqual(Int16.MaxValue, rep.LastMessageNr);
            Assert.AreEqual(Reply.PossibleStatus.Valid, rep.Status);
            Assert.AreEqual("longNote-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", rep.Note);

            rep = new AckNakList(0, 0, Reply.PossibleStatus.Invalid, "");
            Assert.AreEqual(0, rep.FirstMessageNr);
            Assert.AreEqual(0, rep.LastMessageNr);
            Assert.AreEqual(Reply.PossibleStatus.Invalid, rep.Status);
            Assert.AreEqual("", rep.Note);

            // Test Create Factory Method
            AckNakList rep_1 = new AckNakList(543, 18676, Reply.PossibleStatus.Invalid, "Failed to get 4th to 6th sublists completely");
            ByteList bytes = new ByteList();
            rep_1.Encode(bytes);

            AckNakList rep_2 = AckNakList.Create(bytes);
            Assert.IsNotNull(rep_2);

            Assert.AreEqual(rep_1.IsARequest, rep_2.IsARequest);
            Assert.AreEqual(rep_1.MessageNr.ProcessId, rep_2.MessageNr.ProcessId);
            Assert.AreEqual(rep_1.MessageNr.SeqNumber, rep_2.MessageNr.SeqNumber);
            Assert.AreEqual(rep_1.ConversationId.ProcessId, rep_2.ConversationId.ProcessId);
            Assert.AreEqual(rep_1.ConversationId.SeqNumber, rep_2.ConversationId.SeqNumber);

            Assert.AreEqual(rep_1.ReplyType, rep_2.ReplyType);

            Assert.AreEqual(rep_1.FirstMessageNr, rep_2.FirstMessageNr);
            Assert.AreEqual(rep_1.LastMessageNr, rep_2.LastMessageNr);
            Assert.AreEqual(rep_1.Status, rep_2.Status);
            Assert.AreEqual(rep_1.Note, rep_2.Note);
        }
    }
}
