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
    public class PlayerConversationTester
    {
        [TestMethod]
        public void TestSendRequest()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq1, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);

            conversation.SendRequest();

            int timeout = 1000;
            while (!commManager.IncomingAvailable() && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }

            Assert.IsTrue(commManager.IncomingAvailable());
            Envelope incomingEnv = commManager.Receive();
            Assert.IsNotNull(incomingEnv);
            Assert.AreEqual(incomingEnv.Message.MessageNr, incomingEnv.Message.MessageNr);
            Assert.AreEqual(commPlayer.LocalEP, incomingEnv.SendersEP);

            Assert.AreEqual(conversation.Request, request);

            commManager.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestResendRequest()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commPlayer.Start();
            commManager.Start();

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq1, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);

            conversation.SendRequest();

            Assert.AreNotEqual(conversation.myTimer, null);
            
            Thread.Sleep(35000);

            Assert.AreEqual(conversation.State, PlayerConversation.PossibleStates.RequestSent);
            Assert.AreEqual(conversation.NumberOfRetry, 3);
            Assert.AreEqual(conversation.myTimer, null);

            commManager.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestReceiveReply()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commPlayer.Start();
            commManager.Start();

            //Send a message
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq1, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversation.SendRequest();

            Thread.Sleep(1000);

            //Receive a message
            AckNak regReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");
            Envelope reply = Envelope.CreateIncomingEnvelope(regReply, commManager.LocalEP);
            conversation.ReceiveReply(reply);

            Assert.AreEqual(reply, conversation.Reply);
            Assert.AreEqual(PlayerConversation.PossibleStates.ReplyReceived, conversation.State);
            Assert.AreEqual(1, conversation.NumberOfRetry);

            Thread.Sleep(ManagerConversation.Timeout + 1000);
            Assert.AreEqual(null, conversation.myTimer);
            
            commManager.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestIsDuplicateMessage()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commPlayer.Start();
            commManager.Start();

            //Send a message
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq1, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversation.SendRequest();

            Thread.Sleep(1000);

            //Receive a message
            AckNak regReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");
            Envelope reply = Envelope.CreateIncomingEnvelope(regReply, commManager.LocalEP);
            conversation.ReceiveReply(reply);

            //Receive a duplicate message
            Envelope duplicateReply = Envelope.CreateIncomingEnvelope(regReply, commManager.LocalEP);
            
            Assert.IsTrue(conversation.IsDuplicateMessage(duplicateReply));

            commManager.Stop();
            commPlayer.Stop();
        }
    }
}
