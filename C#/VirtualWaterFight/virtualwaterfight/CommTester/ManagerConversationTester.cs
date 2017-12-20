using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

using Common.Messages;
using Common.Communicator;
using Objects;

namespace CommTester
{
    [TestClass]
    public class ManagerConversationTester
    {
        [TestMethod]
        public void TestSendToManagers()
        {
            Communicator commManager1 = new Communicator();
            Communicator commManager2 = new Communicator();
            Communicator commManager3 = new Communicator();
            Communicator commmPlayer = new Communicator();

            commManager1.Start();
            commManager2.Start();
            commManager3.Start();
            commmPlayer.Start();
            
            // Try normal message
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope envelope1 = Envelope.CreateIncomingEnvelope(regReq1, commmPlayer.LocalEP);
            ManagerConversation conManager1 = new ManagerConversation(envelope1, commManager1, commManager2.LocalEP, commManager3.LocalEP);
            //conManager1.SendReplyToManagers(regReq1);
            //conManager1.SendReplyToPlayer(regReq1);
            conManager1.SendReply(regReq1, regReq1);
          
            int timeout = 1000;
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

            Assert.AreEqual(conManager1.NumberOfRetry, 1);
        }
    }
}
