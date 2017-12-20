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
    public class SpecificFightPlayersListRequestTester
    {
        [TestMethod]
        public void SpecificFightPlayersListRequest_Test()
        {
            // Test Public Constructor
            SpecificFightPlayersListRequest req = new SpecificFightPlayersListRequest(809);
            Assert.AreEqual(809, req.FightID);

            req = new SpecificFightPlayersListRequest(Int32.MaxValue);
            Assert.AreEqual(Int32.MaxValue, req.FightID);

            req = new SpecificFightPlayersListRequest(0);
            Assert.AreEqual(0, req.FightID);

            // Test Create Factory Method
            SpecificFightPlayersListRequest req_1 = new SpecificFightPlayersListRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            SpecificFightPlayersListRequest req_2 = SpecificFightPlayersListRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.FightID, req_2.FightID);
        }
    }
}
