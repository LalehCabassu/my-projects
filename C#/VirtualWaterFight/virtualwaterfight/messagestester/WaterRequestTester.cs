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
    public class WaterRequestTester
    {
        [TestMethod]
        public void WaterRequest_Test()
        {
            // Test Public Constructor
            WaterRequest req = new WaterRequest(57, WaterRequest.PossibleSize.Medium);
            Assert.AreEqual(57, req.PercentFilled);
            Assert.AreEqual(WaterRequest.PossibleSize.Medium, req.BalloonSize);

            // Test Create Factory Method
            WaterRequest req_1 = new WaterRequest(87, WaterRequest.PossibleSize.XLarge);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            WaterRequest req_2 = WaterRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PercentFilled, req_2.PercentFilled);
            Assert.AreEqual(req_1.BalloonSize, req_2.BalloonSize);
        }
    }
}
