using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Common.Messages
{
    public class Envelope
    {
        //Took from class notes
        public Message Message { get; set; }
        public IPEndPoint SendersEP { get; set; }
        public IPEndPoint ReceiversEP { get; set; }

        public Envelope() { }

        public static Envelope CreateOutgoingEnvelope(Message message, IPEndPoint targetEP)
        {
            Envelope result = new Envelope();
            result.Message = message;
            result.ReceiversEP = targetEP;
            return result;
        }

        public static Envelope CreateIncomingEnvelope(Message message, IPEndPoint sendersEP)
        {
            Envelope result = new Envelope();
            result.Message = message;
            result.SendersEP = sendersEP;
            return result;
        }

        public bool IsValidToSend
        {
            get
            {
                // TODO: Test for valid message and receiversEP ???
                return true;
            }
        }
    }
}
