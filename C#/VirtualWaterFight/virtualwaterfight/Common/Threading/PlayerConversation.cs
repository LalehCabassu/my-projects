using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

using Common.Messages;
using Common.Communicator;

namespace Common.Threading
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
        public static Int16 RetryLimit = 3;
        public static Int16 Timeout = 10000;
        public Int16 NumberOfRetry = 0;
        public Timer myTimer = null;
        
        private Communicator.Communicator myCommunicator;

        public PlayerConversation(Envelope request, Communicator.Communicator communicator)
        {
            Request = request;
            ConversationID = request.Message.ConversationId;
            myCommunicator = communicator;
        }

        public void SendRequest()
        {
            NumberOfRetry = 0;
            myTimer = new Timer(ResendRequest, null, 0, Timeout);
        }

        private void ResendRequest(object state)
        {
            if (State != PossibleStates.ReplyReceived && NumberOfRetry < RetryLimit)
            {
                myCommunicator.Send(Request);
                State = PossibleStates.RequestSent;
                LastUpdateTime = DateTime.Now;
                NumberOfRetry++;
            }
            else
            {
                if (myTimer != null)
                {
                    myTimer.Dispose();
                    myTimer = null;
                }
            }
        }

        public void ReceiveReply(Envelope reply)
        {
            if (State == PossibleStates.RequestSent)
            {
                State = PossibleStates.ReplyReceived;
                Reply = reply;
                LastUpdateTime = DateTime.Now;
            }

        }

        public bool IsFinished()
        {
            if (State == PossibleStates.ReplyReceived)
                return true;
            return false;
        }

        public bool IsDuplicateMessage(Envelope message)
        {
            if(Reply != null)
                if (message.Message.ConversationId == ConversationID && message.Message.MessageNr == Reply.Message.MessageNr)
                    return true;
            return false;
        }

    }
}
