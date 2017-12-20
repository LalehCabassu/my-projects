using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common.Messages;
using Common.Communicator;
using Common.Threading;
using Objects;

namespace TestVirtualWaterFight
{
    [TestClass]
    public class CommunicatorTester
    {
        [TestMethod]
        public void TestSendAndReceive()
        {
            Common.Communicator.Communicator comm1 = new Common.Communicator.Communicator();
            Common.Communicator.Communicator comm2 = new Common.Communicator.Communicator();

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
    }
}
