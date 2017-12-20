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
    public class PacketizedPlayersListReplyTester
    {
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
    }
}
