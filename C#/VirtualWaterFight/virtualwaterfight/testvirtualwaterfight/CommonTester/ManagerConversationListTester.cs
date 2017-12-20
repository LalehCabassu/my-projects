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

namespace TestVirtualWaterFight.CommonTester
{
    [TestClass]
    public class ManagerConversationListTester
    {
        [TestMethod]
        public void TestAddNewConversation()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start(); 
            commPlayer.Start();

            ManagerConversationList conversationList = new ManagerConversationList(commManager1, commManager2.LocalEP, commManager3.LocalEP);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request);
            
            Assert.AreEqual(1, conversationList.conversationDictionary.Count);

            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request2 = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request2);
            
            Assert.AreEqual(2, conversationList.conversationDictionary.Count);
            
            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestFindConversation()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commPlayer.Start();

            ManagerConversationList conversationList = new ManagerConversationList(commManager1, commManager2.LocalEP, commManager3.LocalEP);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request);

            Assert.AreNotEqual(null, conversationList.FindConversation(regReq.ConversationId));
            
            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestIsExist()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commPlayer.Start();

            ManagerConversationList conversationList = new ManagerConversationList(commManager1, commManager2.LocalEP, commManager3.LocalEP);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request);

            Assert.IsTrue(conversationList.IsExist(regReq.ConversationId));

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commPlayer.Stop();
        }

        [TestMethod]
        public void TestClearConversationList()
        {
            Common.Communicator.Communicator commManager1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager2 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commManager3 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator commPlayer = new Common.Communicator.Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commPlayer.Start();

            ManagerConversationList conversationList = new ManagerConversationList(commManager1, commManager2.LocalEP, commManager3.LocalEP);

            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request);

            Assert.AreEqual(1, conversationList.conversationDictionary.Count);

            MessageNumber.LocalProcessId = 1;
            regReq.ConversationId = MessageNumber.Create();
            Envelope request2 = Envelope.CreateIncomingEnvelope(regReq, commPlayer.LocalEP);
            conversationList.AddNewConversation(request2);
            
            Thread.Sleep(200000);

            conversationList.conversationDictionary.ElementAt(0).Value.LastUpdateTime = DateTime.Now;
            conversationList.conversationDictionary.ElementAt(1).Value.LastUpdateTime = DateTime.Now;

            conversationList.CleanConversationList();

            Thread.Sleep(5000);

            Assert.AreEqual(0, conversationList.conversationDictionary.Count);

            commManager1.Stop();
            commManager2.Stop();
            commManager3.Stop();
            commPlayer.Stop();
        }
    }
}
