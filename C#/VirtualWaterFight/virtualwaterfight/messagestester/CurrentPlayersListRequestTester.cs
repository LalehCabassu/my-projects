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
    public class CurrentPlayersListRequestTester
    {
        [TestMethod]
        public void CurrentPlayersListRequest_Test()
        {
            // Test Public Constructor and Create Factory Method
            CurrentPlayersListRequest req_1 = new CurrentPlayersListRequest();
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            CurrentPlayersListRequest req_2 = CurrentPlayersListRequest.Create(bytes);
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
