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
    public class PlayerLocationRequestTester
    {
        [TestMethod]
        public void PlayerLocationRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            PlayerLocationRequest req_1 = new PlayerLocationRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            PlayerLocationRequest req_2 = PlayerLocationRequest.Create(bytes);
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
