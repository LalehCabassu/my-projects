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
    public class NumberOfFightsRequestTester
    {
        [TestMethod]
        public void NumberOfFightsRequest_Test()
        {
            // Test Public Constructor
            NumberOfFightsRequest req = new NumberOfFightsRequest(283);
            Assert.AreEqual(283, req.PlayerID);

            req = new NumberOfFightsRequest(Int16.MaxValue);
            Assert.AreEqual(Int16.MaxValue, req.PlayerID);

            req = new NumberOfFightsRequest(0);
            Assert.AreEqual(0, req.PlayerID);

            // Test Create Factory Method
            NumberOfFightsRequest req_1 = new NumberOfFightsRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            NumberOfFightsRequest req_2 = NumberOfFightsRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerID, req_2.PlayerID);
        }
    }
}
