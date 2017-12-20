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
    public class JoinFightRequestTester
    {
        [TestMethod]
        public void JoinFightRequest_Tester()
        {
            // Test Public Constructor
            JoinFightRequest req = new JoinFightRequest(810,127, 50);
            Assert.AreEqual(810, req.FightID);
            Assert.AreEqual(127, req.PlayerLocation);
            Assert.AreEqual(50, req.AmountOfWater);


            req = new JoinFightRequest(int.MaxValue, int.MaxValue, Int16.MaxValue);
            Assert.AreEqual(int.MaxValue, req.FightID);
            Assert.AreEqual(int.MaxValue, req.PlayerLocation);
            Assert.AreEqual(Int16.MaxValue, req.AmountOfWater);

            req = new JoinFightRequest(0, 0, 0);
            Assert.AreEqual(0, req.FightID);
            Assert.AreEqual(0, req.PlayerLocation);
            Assert.AreEqual(0, req.AmountOfWater);

            // Test Create Factory Method
            JoinFightRequest req_1 = new JoinFightRequest(201, 35, 25);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            JoinFightRequest req_2 = JoinFightRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.FightID, req_2.FightID);
            Assert.AreEqual(req_1.PlayerLocation, req_2.PlayerLocation);
            Assert.AreEqual(req_1.AmountOfWater, req_2.AmountOfWater);
        }
    }
}
