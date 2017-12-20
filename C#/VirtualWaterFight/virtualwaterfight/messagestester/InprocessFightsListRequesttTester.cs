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
    public class InprocessFightsListRequesttTester
    {
        [TestMethod]
        public void InprocessFightsListRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            InprocessFightsListRequest req_1 = new InprocessFightsListRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            InprocessFightsListRequest req_2 = InprocessFightsListRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);
        }
    }
}
