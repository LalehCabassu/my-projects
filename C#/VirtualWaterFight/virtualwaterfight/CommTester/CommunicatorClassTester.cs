using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Net;
using log4net;
using log4net.Config;

using Common.Threading;
using Common.Messages;
using Common.Communicator;
using Objects;

namespace CommTester
{
    [TestClass]
    public class CommunicatorClassTester
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CommunicatorClassTester));
        
        [TestMethod]
        public void TestSendAndReceive()
        {
            // Setup log4net using the app.config
            XmlConfigurator.Configure();

            //ProcessSettings settings = new ProcessSettings();

            log.Info("Start up Command Tester");


            log.Info("Edits up communicator threads");
            Communicator comm1 = new Communicator();
            Communicator comm2 = new Communicator();

            log.Info("Start communicator threads");
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


            // Try sending duplicate messages by send the same exact message to the communicator; expect only one message to
            //      be available from the communicator

            // Try sending message in both directions simulateously; expect it to work with no problems.

            // Try sending multiple messages before any receivings; expect it to work and the receiving communication to have
            //      available messages

            // Try receiving message with no sending or lost message; expect no message to be available and the GetIncoming
            //      to reutrn a null;

            comm1.Stop();
            comm2.Stop();
        }
    }
}
