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
    public class DecrementNumberOfBalloonsRequestTester
    {
        [TestMethod]
        public void DecrementNumberOfBalloonsRequest_Test()
        {
            // Test Public Constructor
            DecrementNumberOfBalloonsRequest req = new DecrementNumberOfBalloonsRequest(127);
            Assert.AreEqual(127, req.PlayerID);
            
            req = new DecrementNumberOfBalloonsRequest(Int16.MaxValue);
            Assert.AreEqual(Int16.MaxValue, req.PlayerID);
           
            req = new DecrementNumberOfBalloonsRequest(0);
            Assert.AreEqual(0, req.PlayerID);
            
            // Test Create Factory Method
            DecrementNumberOfBalloonsRequest req_1 = new DecrementNumberOfBalloonsRequest(21);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            DecrementNumberOfBalloonsRequest req_2 = DecrementNumberOfBalloonsRequest.Create(bytes);
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
