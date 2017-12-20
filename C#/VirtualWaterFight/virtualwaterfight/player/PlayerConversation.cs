using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

using Common.Communicator;
using Common.Messages;

namespace Player
{
    public class PlayerConversation
    {
        public enum PossibleStates
        {
            RequestSent = 1, 
            ReplyReceived = 2
        }
        public MessageNumber ConversationID;
        public PossibleStates State;
        public Envelope Request;
        public Envelope Reply;
        public DateTime LastUpdateTime;
        public Int16 RetryLimit = 3;
        public Int16 Timeout = 10000;
        public Int16 NumberOfRetry = 0;
        
        private Timer myTimer = null;
        private Communicator myCommunicator;

        public PlayerConversation(Envelope request, Communicator communicator)
        {
            Request = request;
            ConversationID = request.Message.ConversationId;
            myCommunicator = communicator;
        }

        public void SendRequest()
        {
            myCommunicator.Send(Request);
            LastUpdateTime = DateTime.Now;
            State = PossibleStates.RequestSent;
            NumberOfRetry = 1;
            myTimer = new Timer(ResendRequest, null, 30000, 10000);
        }

        private void ResendRequest(object state)
        {
            if(State != PossibleStates.ReplyReceived && NumberOfRetry <= RetryLimit)
            {
                myCommunicator.Send(Request);
                LastUpdateTime = DateTime.Now;
                NumberOfRetry++;
            }
        }

        public void ReceiveReply(Envelope reply)
        {
            if (State == PossibleStates.RequestSent)
            {
                Reply = reply;
                State = PossibleStates.ReplyReceived;
                LastUpdateTime = DateTime.Now;
            }
        }

        public bool IsDuplicateMessage(Envelope message)
        {
            if (message.Message.ConversationId == ConversationID && message.Message.MessageNr == Reply.Message.MessageNr)
                return true;
            return false;
        }

        public bool IsOldMessage(Envelope message)
        {
            if (message.Message.ConversationId == ConversationID && message.Message.MessageNr < Reply.Message.MessageNr)
                return true;
            return false;
        }
    }
}
