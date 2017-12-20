using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common;
using Common.Messages;
using Common.Communicator;
using Common.Threading;
using Objects;
using Player;
using FightManager;
using BalloonManager;
using WaterManager;

namespace TestVirtualWaterFight
{
    [TestClass]
    public class ManagerConversationTester
    {
        [TestMethod]
        public void TestSendReply()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commmPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conversation = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);
            conversation.SendReply(regReq1, regReq1);

            Assert.AreEqual(conversation.PlayerReply, regReq1);
            Assert.AreEqual(conversation.ManagersReply, regReq1);

            int timeout = 3000;
            while (!commManager2.IncomingAvailable() && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }

            Assert.IsTrue(commManager2.IncomingAvailable());
            Envelope envelope2 = commManager2.Receive();
            Assert.IsNotNull(envelope2);
            Assert.AreEqual(envelope1.Message.MessageNr, envelope2.Message.MessageNr);
            Assert.AreEqual(commManager1.LocalEP.Port, envelope2.SendersEP.Port);

            Assert.IsTrue(commManager3.IncomingAvailable());
            Envelope envelope3 = commManager3.Receive();
            Assert.IsNotNull(envelope3);
            Assert.AreEqual(envelope1.Message.MessageNr, envelope3.Message.MessageNr);
            Assert.AreEqual(commManager1.LocalEP.Port, envelope3.SendersEP.Port);

            Assert.IsTrue(commmPlayer.IncomingAvailable());
            Envelope envelope4 = commmPlayer.Receive();
            Assert.IsNotNull(envelope4);
            Assert.AreEqual(envelope1.Message.MessageNr, envelope4.Message.MessageNr);
            Assert.AreEqual(commManager1.LocalEP.Port, envelope4.SendersEP.Port);

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commmPlayer.Stop();
        }

        [TestMethod]
        public void TestResendReply()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commmPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conversation = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);
            conversation.SendReply(regReq1, regReq1);

            Assert.AreNotEqual(conversation.myTimer, null);

            Thread.Sleep(35000);

            Assert.AreEqual(conversation.State, ManagerConversation.PossibleStates.ReplySent);
            Assert.AreEqual(conversation.NumberOfRetry, 3);
            Assert.AreEqual(conversation.myTimer, null);

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commmPlayer.Stop();
        }

        [TestMethod]
        public void TestReceiveAckNak_TwoManagers()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commmPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conversation = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);
            conversation.SendReply(regReq1, regReq1);

            Thread.Sleep(1000);

            conversation.ReceiveAckNak(commManager2.LocalEP, true);
            Assert.AreEqual(ManagerConversation.PossibleStates.FirstAckNakReceived, conversation.State);

            conversation.ReceiveAckNak(commManager3.LocalEP, true);
            Assert.AreEqual(ManagerConversation.PossibleStates.Finished, conversation.State);

            Assert.AreEqual(1, conversation.NumberOfRetry);

            Thread.Sleep(ManagerConversation.Timeout + 1000);
            Assert.AreEqual(null, conversation.myTimer);

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commmPlayer.Stop();
        }

        [TestMethod]
        public void TestReceiveAckNak_OneManager()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commmPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conversation = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);
            conversation.SendReply(regReq1, regReq1);

            Thread.Sleep(1000);

            conversation.ReceiveAckNak(commManager2.LocalEP, false);
            Assert.AreEqual(ManagerConversation.PossibleStates.Finished, conversation.State);

            Assert.AreEqual(1, conversation.NumberOfRetry);

            Thread.Sleep(ManagerConversation.Timeout + 1000);
            Assert.AreEqual(null, conversation.myTimer);

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commmPlayer.Stop();
        }

        [TestMethod]
        public void TestIsDuplicateMessage()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commmPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conversation = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);

            Envelope duplicateRequest = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            
            Assert.IsTrue(conversation.IsDuplicateMessage(duplicateRequest));

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commmPlayer.Stop();
        }

    }
}
