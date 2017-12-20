using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common.Messages;
using Common.Communicator;
using Objects;

namespace TestVirtualWaterFight
{
    [TestClass]
    public class CommunicatorTester
    {
        [TestMethod]
        public void TestSendAndReceive()
        {
            Communicator comm1 = new Communicator();
            Communicator comm2 = new Communicator();

            comm1.Start();
            comm2.Start();
            //comm2.LocalEP.Port = 52354;
            // Try normal message
            Location myLocation = new Location(1, 2);
            //RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            HitThrowerReply mes1 = new HitThrowerReply(2, 3, Reply.PossibleStatus.Valid, "Fight");
            Envelope envelope1 = new Envelope();
            //envelope1.Message = regReq1;
            envelope1.Message = mes1;
            envelope1.ReceiversEP = comm2.LocalEP;

            comm1.Send(envelope1);

            int timeout = 1000;
            while (!comm2.IncomingAvailable() && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }

            Assert.IsTrue(comm2.IncomingAvailable());
            Envelope envelope2 = comm2.Receive();
            Assert.IsNotNull(envelope2);
            Assert.AreEqual(envelope1.Message.MessageNr, envelope2.Message.MessageNr);
            Assert.AreEqual(comm1.LocalEP.Port, envelope2.SendersEP.Port);

            // Try to send null envelope
            try
            {
                comm1.Send(null);
                Assert.Fail("Exception not thrown as expected");
            }
            catch (ApplicationException) { }
            catch
            {
                Assert.Fail("Unexpected exception");
            }

            // Try to send envelope with no receiversEP; expect an exception
            envelope1.ReceiversEP = null;
            try
            {
                comm1.Send(envelope1);
                Assert.Fail("Exception not thrown as expected");
            }
            catch (ApplicationException) { }
            catch
            {
                Assert.Fail("Unexpected exception");
            }


            // Try to send an envelope with no message; expect an exception
            envelope1.Message = null;
            envelope1.ReceiversEP = comm2.LocalEP;
            try
            {
                comm1.Send(envelope1);
                Assert.Fail("Exception not thrown as expected");
            }
            catch (ApplicationException) { }
            catch
            {
                Assert.Fail("Unexpected exception");
            }

            comm1.Stop();
            comm2.Stop();
        }

        [TestMethod]
        public void TestReliableSendForPlayer()
        {
            Communicator commManager = new Communicator();
            Communicator commPlayer = new Communicator();

            commManager.Start();
            commPlayer.Start();

            // Try normal message
            Location myLocation = new Location(1, 2);
            RegistrationRequest regReq1 = new RegistrationRequest("Laleh", 30, true, myLocation);
            Envelope request = Envelope.CreateOutgoingEnvelope(regReq1, commManager.LocalEP);
            PlayerConversation conPlayer = new PlayerConversation(request, commPlayer);
            
            conPlayer.SendRequest();

            int timeout = 6000;
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

        }

        [TestMethod]
        public void TestReliableSendForManagers()
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
