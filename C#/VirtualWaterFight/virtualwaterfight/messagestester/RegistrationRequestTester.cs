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
    public class RegistrationRequestTester
    {
        [TestMethod]
        public void RegistrationRequest_Test()
        {
            // Test Public Constructor
            RegistrationRequest req = new RegistrationRequest("Joe", 17, true, 100);
            Assert.AreEqual("Joe", req.Name);
            Assert.AreEqual(17, req.Age);
            Assert.AreEqual(true, req.Gender);
            Assert.AreEqual(100, req.Location);

            req = new RegistrationRequest("longUsername-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()",
                                                            Int16.MaxValue, true, Int32.MaxValue);
            Assert.AreEqual("longUsername-ABCDEFGHIJKLMNOPQRSTUVWXYZ-0123456789'|;:',.=-_+!@#$%^&*()", req.Name);
            Assert.AreEqual(Int16.MaxValue, req.Age);
            Assert.AreEqual(true, req.Gender);
            Assert.AreEqual(Int32.MaxValue, req.Location);
            
            req = new RegistrationRequest("", 0, false, 0);
            Assert.AreEqual("", req.Name);
            Assert.AreEqual(0, req.Age);
            Assert.AreEqual(false, req.Gender);
            Assert.AreEqual(0, req.Location);

            req = new RegistrationRequest(null, 0, false, 0);
            Assert.AreEqual(null, req.Name);
            Assert.AreEqual(0, req.Age);
            Assert.AreEqual(false, req.Gender);
            Assert.AreEqual(0, req.Location);

            // Test Create Factory Method
            RegistrationRequest req_1 = new RegistrationRequest("Henry", 21, true, 205);
            ByteList bytes = new ByteList();
            req_1.Encode(bytes);

            RegistrationRequest req_2 = RegistrationRequest.Create(bytes);
            Assert.IsNotNull(req_2);
            
            Assert.AreEqual(req_1.IsARequest, req_2.IsARequest);
            Assert.AreEqual(req_1.MessageNr.ProcessId, req_2.MessageNr.ProcessId);
            Assert.AreEqual(req_1.MessageNr.SeqNumber, req_2.MessageNr.SeqNumber);
            Assert.AreEqual(req_1.ConversationId.ProcessId, req_2.ConversationId.ProcessId);
            Assert.AreEqual(req_1.ConversationId.SeqNumber, req_2.ConversationId.SeqNumber);

            Assert.AreEqual(req_1.RequestType, req_2.RequestType);

            Assert.AreEqual(req_1.Name, req_2.Name);
            Assert.AreEqual(req_1.Age, req_2.Age);
            Assert.AreEqual(req_1.Gender, req_2.Gender);
            Assert.AreEqual(req_1.Location, req_2.Location);
            
        }
/*
        [TestMethod]
        public void RegistrationRequest_EncodingAndDecoding()
        {
            Message m1 = new RegistrationRequest("Joe", 17, true, 100);
            m1.MessageNr = MessageNumber.Create(10, 14);
            m1.ConversationId = MessageNumber.Create(10, 12);
            ByteList bytes = new ByteList();
            m1.Encode(bytes);

            #region Assertions
            byte[] buffer = bytes.ToBytes();
            byte[] expectedBuffer = new byte[]
                                {0, 101, 0, 41,                                 // Login Request Object Header
                                    0, 100, 0, 22,                              // Request Object Header
                                        0, 1, 0, 17,                            // Message Object Header
                                            1,                                  // Is a request
                                            0, 2, 0, 4,                         // MessageNr Object Header
                                                0, 10,                          // ProcessId
                                                0, 14,                          // Sequence Number
                                            0, 2, 0, 4,                         // ConversionId Object Header
                                                0, 10,                          // ProcessId
                                                0, 12,                          // Sequence Number
                                        1,                                      // Request type
                                    0, 6, 74, 0, 111, 0, 101, 0,                // Username
                                    0, 2, 0, 17,                                // Age
                                    0, 1, 1,                                    // Gender
                                    0, 2, 0, 100                                // Location
                                };

            Assert.AreEqual(expectedBuffer.Length, buffer.Length);
            for (int idx = 0; idx < buffer.Length; idx++)
            {
                Console.WriteLine("Comparing byte #" + idx.ToString());
                Assert.AreEqual(expectedBuffer[idx], buffer[idx]);
            }

            // Try decoding the bytes from the encoding to see if we get an equivilent object
            bytes.ResetRead();
            Message m2 = Message.Create(bytes);
            Assert.AreEqual(m1.GetType(), m2.GetType());
            Assert.AreEqual(m1.IsARequest, m2.IsARequest);
            Assert.AreEqual(m1.MessageNr.ProcessId, m2.MessageNr.ProcessId);
            Assert.AreEqual(m1.MessageNr.SeqNumber, m2.MessageNr.SeqNumber);
            Assert.AreEqual(m1.ConversationId.ProcessId, m2.ConversationId.ProcessId);
            Assert.AreEqual(m1.ConversationId.SeqNumber, m2.ConversationId.SeqNumber);

            RegistrationRequest regReq_1 = m1 as RegistrationRequest;
            RegistrationRequest regReq_2 = m2 as RegistrationRequest;

            Assert.AreEqual(regReq_1.RequestType, regReq_2.RequestType);

            Assert.AreEqual(regReq_1.Name, regReq_2.Name);
            Assert.AreEqual(regReq_1.Age, regReq_2.Age);
            Assert.AreEqual(regReq_1.Gender, regReq_2.Gender);
            Assert.AreEqual(regReq_1.Location, regReq_2.Location);

            #endregion
        }
 * */
    }
}
