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

namespace TestVirtualWaterFight.Communicator
{
    [TestClass]
    public class PlayerConversationListTester
    {
        [TestMethod]
        public void TestAddNewConversation()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            PlayerConversationList conversationList = new PlayerConversationList(commPlayer);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            
            conversationList.AddNewConversation(regReq, commManager.LocalEP);
            Assert.AreEqual(1, conversationList.conversationDictionary.Count);
            
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversationList.AddNewConversation(conversation);
            
            Assert.AreEqual(2, conversationList.conversationDictionary.Count);
            
            commPlayer.Stop();
            commManager.Stop();
        }

        [TestMethod]
        public void TestFindConversation()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            PlayerConversationList conversationList = new PlayerConversationList(commPlayer);
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversationList.AddNewConversation(conversation);
            
            Assert.AreEqual(conversation, conversationList.FindConversation(regReq.ConversationId));
            
            commPlayer.Stop();
            commManager.Stop();
        }

        [TestMethod]
        public void TestIsExist()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            PlayerConversationList conversationList = new PlayerConversationList(commPlayer);
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversationList.AddNewConversation(conversation);
            
            Assert.IsTrue(conversationList.IsExist(regReq.ConversationId));

            commPlayer.Stop();
            commManager.Stop();
        }

        [TestMethod]
        public void TestChangeConversationID()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            PlayerConversationList conversationList = new PlayerConversationList(commPlayer);
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            conversationList.AddNewConversation(regReq, commManager.LocalEP);
            
            MessageNumber.LocalProcessId = 1;
            MessageNumber newConversationID = MessageNumber.Create();
            conversationList.ChangeConversationID(regReq.ConversationId, newConversationID);

            Assert.IsTrue(conversationList.conversationDictionary.ContainsKey(newConversationID));

            commPlayer.Stop();
            commManager.Stop();
        }

        [TestMethod]
        public void TestClearConversationList()
        {
            Common.Communicator.Communicator commManager = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager.Start();
            commPlayer.Start();

            PlayerConversationList conversationList = new PlayerConversationList(commPlayer);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            conversationList.AddNewConversation(regReq, commManager.LocalEP);
            
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq, commManager.LocalEP);
            PlayerConversation conversation = new PlayerConversation(request, commPlayer);
            conversationList.AddNewConversation(conversation);

            Thread.Sleep(200000);

            conversationList.conversationDictionary.ElementAt(0).Value.LastUpdateTime = DateTime.Now;
            conversationList.conversationDictionary.ElementAt(1).Value.LastUpdateTime = DateTime.Now;
 
            conversationList.CleanConversationList();

            Thread.Sleep(3000);

            Assert.AreEqual(0, conversationList.conversationDictionary.Count);

            commPlayer.Stop();
            commManager.Stop();
        }

    }
}