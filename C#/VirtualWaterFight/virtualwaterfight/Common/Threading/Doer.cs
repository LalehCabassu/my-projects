using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using log4net.Config;
using System.Net;

using Common.Communicator;
using Common.Messages;

namespace Common.Threading
{
    public class Doer : BackgroundThread
    {
        #region Data members and Getter/Setter
        //public MessageQueue incomingQueue_Registration;                 

        public Communicator.Communicator myCommunicator;

        //private Envelope incomingMessage;
        private static readonly ILog log = LogManager.GetLogger(typeof(Doer));
        //public ConversationList MyConversationList;
        #endregion

        #region Public Methods
        public Doer()
        {
        }

        public Doer(Communicator.Communicator communicator)
        {
            myCommunicator = communicator;
        }

        public override string ThreadName()
        {
            return "Doer";
        }

        public virtual bool MessageAvailable()
        {
            return myCommunicator.IncomingAvailable();
        }

        public virtual void Send(Message outGoingMessage, IPEndPoint targetEP)
        {
            log.InfoFormat("Send", ThreadName());
            Envelope outGoingEnvelope = Envelope.CreateOutgoingEnvelope(outGoingMessage, targetEP);
            myCommunicator.Send(outGoingEnvelope);
        }

        public virtual void Send(Envelope outGoingEnvelope)
        {
            log.InfoFormat("Send", ThreadName());
            myCommunicator.Send(outGoingEnvelope);
        }

        public virtual Envelope Receive()
        {
            log.InfoFormat("Receive", ThreadName());
            return myCommunicator.Receive();
        }

        public virtual void DoProtocol(Envelope message)
        {
        }

        public bool IsRequest(Envelope message)
        {
            return message.Message.IsARequest;
        }

        public Request.PossibleTypes GetRequestType(Envelope message)
        {
            log.InfoFormat("getRequestType", ThreadName());

            Request tempReq = message.Message as Request;
            return tempReq.RequestType;
        }

        public Reply.PossibleTypes GetReplyType(Envelope message)
        {
            log.InfoFormat("getReplyType", ThreadName());
            
            Reply tempRep = message.Message as Reply;
            return tempRep.ReplyType;
        }

        public string GetReplyNote(Envelope message)
        {
            Reply tempRep = message.Message as Reply;
            return tempRep.Note;
        }
        #endregion

        #region Private Methods
        protected override void Process()
        {
            
        }
        #endregion
    }
}
