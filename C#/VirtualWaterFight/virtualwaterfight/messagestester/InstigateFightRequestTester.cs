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
    public class InstigateFightRequestTester
    {
        [TestMethod]
        public void InstigateFightRequest_Test()
        {
            // Test Public Constructor
            InstigateFightRequest req = new InstigateFightRequest(810, 127);
            Assert.AreEqual(810, req.PlayerLocation);
            Assert.AreEqual(127, req.AmountOfWater);

            req = new InstigateFightRequest(int.MaxValue, Int16.MaxValue);
            Assert.AreEqual(int.MaxValue, req.PlayerLocation);
            Assert.AreEqual(Int16.MaxValue, req.AmountOfWater);

            req = new InstigateFightRequest(0, 0);
            Assert.AreEqual(0, req.PlayerLocation);
            Assert.AreEqual(0, req.AmountOfWater);

            // Test Create Factory Method
            InstigateFightRequest req_1 = new InstigateFightRequest(201, 35);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            InstigateFightRequest req_2 = InstigateFightRequest.Create(bytes);
            Assert.IsNotNull(req_2);

            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.PlayerLocation, req_2.PlayerLocation);
            Assert.AreEqual(req_1.AmountOfWater, req_2.AmountOfWater);
        }
    }
}
