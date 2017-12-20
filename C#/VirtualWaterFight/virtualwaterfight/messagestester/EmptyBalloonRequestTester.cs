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
    public class EmptyBalloonRequestTester
    {
        [TestMethod]
        public void EmptyBalloonRequest_Test()
        {
            // Test Public Constructor
            EmptyBalloonRequest req = new EmptyBalloonRequest(EmptyBalloonRequest.PossibleSize.Medium, EmptyBalloonRequest.PossibleColor.Brown);
            Assert.AreEqual(EmptyBalloonRequest.PossibleSize.Medium, req.Size);
            Assert.AreEqual(EmptyBalloonRequest.PossibleColor.Brown, req.Color);
            
            // Test Create Factory Method
            EmptyBalloonRequest req_1 = new EmptyBalloonRequest(EmptyBalloonRequest.PossibleSize.Large, EmptyBalloonRequest.PossibleColor.Green);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            EmptyBalloonRequest req_2 = EmptyBalloonRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.Size, req_2.Size);
            Assert.AreEqual(req_1.Color, req_2.Color);
        }
    }
}
