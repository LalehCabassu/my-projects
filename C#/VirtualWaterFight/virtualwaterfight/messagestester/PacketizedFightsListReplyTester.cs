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
    public class PacketizedFightsListReplyTester
    {
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
    }
}
